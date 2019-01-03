using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Base.Logic
{
    public class SearchLogic : ISearchLogic
    {
        public Task<IEnumerable<PhraseLocation>> SearchWordsAsync(IEnumerable<string> filePaths, string phrase, IUnityContainer container)
        {

            return Task.Run(() =>
            {
                List<PhraseLocation> results = new List<PhraseLocation>();

                Parallel.ForEach(filePaths, (path) =>
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
    }
}