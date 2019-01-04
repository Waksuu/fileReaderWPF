using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using Unity.Attributes;

namespace fileReaderWPF.Base.Logic
{
    public class SearchLogic : ISearchLogic
    {
        [Dependency]
        public Lazy<IFolderRepository> FolderRepository { get; set; }

        public Task<IEnumerable<PhraseLocation>> SearchWordsInFilesAsync(IEnumerable<string> extensions, string phrase, string folderPath, IUnityContainer container)
        {
            return Task.Run(() =>
            {
                ValidateInputs(extensions, phrase, folderPath, container);


                ISpecification<string> extensionSpecification = SpecificationHelper.SpecifyExtensions(extensions);
                var filesForPath = FolderRepository.Value.GetFilesForPath(folderPath, extensionSpecification);

                List<PhraseLocation> results = new List<PhraseLocation>();

                Parallel.ForEach(filesForPath, (path) =>
                {
                    IFileReaderHelper FileReaderHelper = container.Resolve<IFileReaderHelper>(Path.GetExtension(path));

                    var phraseLocations = FileReaderHelper.GetPhraseLocationsFromFile(path, @"\b" + phrase + @"\b").ToList();
                    lock (results)
                    {
                        results.AddRange(phraseLocations);
                    }
                });

                return results.AsEnumerable();
            });
        }

        private void ValidateInputs(IEnumerable<string> extensions, string phrase, string folderPath, IUnityContainer container)
        {
            if (extensions is null || !extensions.Any())
                throw new ArgumentNullException(nameof(extensions));

            if (phrase is null)
                throw new ArgumentNullException(nameof(phrase));

            if (string.IsNullOrWhiteSpace(folderPath))
                throw new ArgumentNullException(nameof(folderPath));

            if (container is null)
                throw new ArgumentNullException(nameof(container));
        }
    }
}