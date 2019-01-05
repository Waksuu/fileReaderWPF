using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Mock.Helpers;
using fileReaderWPF.Mock.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ProvidesCorrectResultForCorrectData()
        {
            // act
            var result = _searchLogicService.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, sampleFolderPath).Result.ToList();

            // assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual(MockFileReaderHelper.SampleExisitingText, result[0].Sentence);
            Assert.AreEqual(MockFileReaderHelper.SampleExistingPath, result[0].Path);
        }

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ThrowsErrorWhenExtensionsIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogicService.SearchWordsInFilesAsync(null, samplePhrase, sampleFolderPath), CreateExpectedMessage("extensions"));
        }

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ThrowsErrorWhenExtensionsAreEmpty()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogicService.SearchWordsInFilesAsync(new List<string>(), samplePhrase, sampleFolderPath), CreateExpectedMessage("extensions"));
        }

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ThrowsErrorWhenSearchPhraseIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogicService.SearchWordsInFilesAsync(sampleExtensions, null, sampleFolderPath), CreateExpectedMessage("phrase"));
        }

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ThrowsErrorWhenPathIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogicService.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, null), CreateExpectedMessage("folderPath"));
        }

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync_ThrowsErrorWhenPathIsWhitespaceOnly()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogicService.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, " "), CreateExpectedMessage("folderPath"));
        }

        private string CreateExpectedMessage(string parameterName) => @"Value cannot be null.
Parameter name: " + parameterName;
    }
}