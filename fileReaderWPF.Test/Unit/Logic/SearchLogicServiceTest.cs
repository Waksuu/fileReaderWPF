using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Mock.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Test.Unit.Logic
{
    [TestClass]
    public class SearchLogicServiceTest
    {
        private static readonly IList<string> sampleExtensions = new List<string>()
            {
                ".txt",
                ".docx",
            };
        private static readonly string samplePhrase = "doesn't matter";
        private static readonly string sampleFolderPath = @"doesn't matter";

        private ISearchLogicService _searchLogicService;

        [TestInitialize]
        public void BaseInit()
        {
            var mockFolderRepository = new Lazy<IFolderRepository>(() => new MockFolderRepository());
            var mockFileReaderHelperFactory = new MockFileReaderHelperFactory();

            _searchLogicService = new SearchLogicService(mockFolderRepository, mockFileReaderHelperFactory);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync()
        {
            // act
            var result = _searchLogicService.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, sampleFolderPath).Result.ToList();

            // assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Sample text", result[0].Sentence);
            Assert.AreEqual("Sample path", result[0].Path);
        }
    }
}
