using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Services
{
    public class SearchLogicService
    {
        private Lazy<ISearchLogic> _searchLogic;

        private void ResolveDependencies()
        {
            var diContainter = ServiceLocator.WpfContainer;
            _searchLogic = diContainter.Resolve<Lazy<ISearchLogic>>();
        }

        public SearchLogicService() => ResolveDependencies();

        public Task<IEnumerable<PhraseLocation>> FindSentencesInFolderPath(IEnumerable<string> extensions, string phrase, string folderPath) => _searchLogic.Value.SearchWordsInFilesAsync(extensions, phrase, folderPath);
    }
}