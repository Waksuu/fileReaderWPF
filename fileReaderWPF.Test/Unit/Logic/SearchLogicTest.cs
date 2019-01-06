using fileReaderWPF.Base.Exceptions;
using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Mock.Helpers;
using fileReaderWPF.Mock.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MSTestExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace fileReaderWPF.Test.Unit.Logic
{
    [TestClass]
    public class SearchLogicTest
    {
        private const string EN_US = "en-us";

        private static readonly IList<string> sampleExtensions = new List<string>()
            {
                ".txt",
                ".docx",
            };

        private static readonly string samplePhrase = "doesn't matter";
        private static readonly string sampleFolderPath = @"doesn't matter";
        private static readonly string notExistingFolderPath = @".\notExistingPath";

        private ISearchLogic _searchLogic;

        [TestInitialize]
        public void BaseInit()
        {
            SetUICulture(EN_US);

            var mockFolderRepository = new Lazy<IFolderRepository>(() => new MockFolderRepository());
            var mockFileReaderHelperFactory = new MockFileReaderHelperFactory();

            _searchLogic = new SearchLogic(mockFolderRepository, mockFileReaderHelperFactory);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ChecksIfAmountOfFilesIsCorrect()
        {
            // act
            var result = _searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, sampleFolderPath).Result.ToList();

            // assert
            Assert.AreEqual(5, result.Count);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ChecksIfSentenceIsCorrect()
        {
            // act
            var result = _searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, sampleFolderPath).Result.ToList();

            // assert
            Assert.AreEqual(MockFileReaderHelper.SampleExisitingText, result[0].Sentence);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ChecksIfPathIsCorrect()
        {
            // act
            var result = _searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, sampleFolderPath).Result.ToList();

            // assert
            Assert.AreEqual(MockFileReaderHelper.SampleExistingPath, result[0].Path);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenExtensionsIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogic.SearchWordsInFilesAsync(null, samplePhrase, sampleFolderPath), CreateExpectedMessage("extensions"));
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenExtensionsAreEmpty()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogic.SearchWordsInFilesAsync(new List<string>(), samplePhrase, sampleFolderPath), CreateExpectedMessage("extensions"));
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenSearchPhraseIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogic.SearchWordsInFilesAsync(sampleExtensions, null, sampleFolderPath), CreateExpectedMessage("phrase"));
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenPathIsNull()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, null), CreateExpectedMessage("folderPath"));
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenPathIsWhitespaceOnly()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<ArgumentNullException>(_searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, " "), CreateExpectedMessage("folderPath"));
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenPathDoesNotExist()
        {
            // arrange
            var mockFolderRepositoryRetruningNull = new Mock<IFolderRepository>();
            mockFolderRepositoryRetruningNull.Setup(x => x.GetFilesForPath(It.IsAny<string>(), It.IsAny<ISpecification<string>>())).Returns((IEnumerable<string>)null);
            var lazyMockFolderRepositoryRetruningNull = new Lazy<IFolderRepository>(() => mockFolderRepositoryRetruningNull.Object);

            var mockFileReaderHelperFactory = new Mock<IFileReaderHelperFactory>();
            mockFileReaderHelperFactory.Setup(x => x.GetFileReaderHelper("")).Returns(new MockFileReaderHelper());

            var searchLogic = new SearchLogic(lazyMockFolderRepositoryRetruningNull, mockFileReaderHelperFactory.Object);

            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<InvalidOperationException>(searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, notExistingFolderPath), Base.Properties.Resources.DirectoryDoesntExist);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenThereAreNoMatchingFilesInPath()
        {
            // arrange
            var mockFolderRepositoryRetruningNull = new Mock<IFolderRepository>();
            mockFolderRepositoryRetruningNull.Setup(x => x.GetFilesForPath(It.IsAny<string>(), It.IsAny<ISpecification<string>>())).Returns(new List<string>());
            var lazyMockFolderRepositoryRetruningNull = new Lazy<IFolderRepository>(() => mockFolderRepositoryRetruningNull.Object);

            var mockFileReaderHelperFactory = new Mock<IFileReaderHelperFactory>();
            mockFileReaderHelperFactory.Setup(x => x.GetFileReaderHelper("")).Returns(new MockFileReaderHelper());

            var searchLogic = new SearchLogic(lazyMockFolderRepositoryRetruningNull, mockFileReaderHelperFactory.Object);

            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<EmptyFolderException>(searchLogic.SearchWordsInFilesAsync(sampleExtensions, samplePhrase, notExistingFolderPath), Base.Properties.Resources.EmptyFolderException);
        }
        #region HelperMethods

        private string CreateExpectedMessage(string parameterName) => @"Value cannot be null.
Parameter name: " + parameterName;

        private static void SetUICulture(string uiCulture) => Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);

        #endregion HelperMethods
    }
}