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

namespace fileReaderWPF.Base.Logic
{
    public class SearchLogic : ISearchLogic
    {
        private readonly Lazy<IFolderRepository> _folderRepository;
        private readonly IFileReaderHelperFactory _fileReaderHelperFactory;

        public SearchLogic(Lazy<IFolderRepository> folderRepository, IFileReaderHelperFactory fileReaderHelperFactory)
        {
            _folderRepository = folderRepository;
            _fileReaderHelperFactory = fileReaderHelperFactory;
        }

        public Task<IEnumerable<PhraseLocation>> SearchWordsInFilesAsync(IEnumerable<string> extensions, string phrase, string folderPath) => Task.Run(() => SearchWordsInFiles(extensions, phrase, folderPath));

        private IEnumerable<PhraseLocation> SearchWordsInFiles(IEnumerable<string> extensions, string phrase, string folderPath)
        {
            ValidateExtensions(extensions);
            ValidateSearchPhrase(phrase);
            ValidateFolderPath(folderPath);

            var results = new List<PhraseLocation>();

            Parallel.ForEach(GetFilesForPath(extensions, folderPath), (path) =>
            {
                List<PhraseLocation> phraseLocations = GetPhraseLocationsFromFile(phrase, path);

                if (phraseLocations.Any())
                {
                    lock (results)
                    {
                        results.AddRange(phraseLocations);
                    }
                }
            });

            return results.AsEnumerable();
        }

        #region Validations

        private static void ValidateExtensions(IEnumerable<string> extensions)
        {
            if (extensions is null || !extensions.Any())
            {
                throw new ArgumentNullException(nameof(extensions));
            }
        }

        private static void ValidateSearchPhrase(string phrase)
        {
            if (phrase is null)
            {
                throw new ArgumentNullException(nameof(phrase));
            }
        }

        private static void ValidateFolderPath(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
            {
                throw new ArgumentNullException(nameof(folderPath));
            }
        }

        #endregion Validations

        private IEnumerable<string> GetFilesForPath(IEnumerable<string> extensions, string folderPath)
        {
            ISpecification<string> extensionSpecification = SpecificationHelper.SpecifyExtensions(extensions);
            return _folderRepository.Value.GetFilesForPath(folderPath, extensionSpecification);
        }

        private List<PhraseLocation> GetPhraseLocationsFromFile(string phrase, string path)
        {
            IFileReaderHelper FileReaderHelper = _fileReaderHelperFactory.GetFileReaderHelper(Path.GetExtension(path));
            return FileReaderHelper.GetPhraseLocationsFromFile(path, @"\b" + phrase + @"\b").ToList();
        }
    }
}