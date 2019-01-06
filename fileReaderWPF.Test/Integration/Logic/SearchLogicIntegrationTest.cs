using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;
using System.Collections.Generic;
using System.Linq;
using System;
using fileReaderWPF.Base.Exceptions;

namespace fileReaderWPF.Test.Real.Logic
{
    [TestClass]
    public class SearchLogicIntegrationTest : IntegrationTestBase
    {
        private static readonly IList<string> sampleExtensions = new List<string>()
            {
                ".txt",
                ".pdf",
                ".docx",
            };
        private static readonly string sampleSearchPhrase = "is always the";
        private static readonly string existingFolderPath = @".\TestResources";
        private static readonly string existingFolderPathWithNoMatchingExtensions = @".\TestResources\FolderWithNoMatchingExtension";
        private static readonly string notExistingFolderPath = @".\notExistingPath";

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync()
        {
            // act
            var result = SearchLogic.SearchWordsInFilesAsync(sampleExtensions, sampleSearchPhrase, existingFolderPath).Result.ToList();

            // assert
            foreach (var item in result)
            {
                Assert.IsTrue(item.Sentence.Contains(sampleSearchPhrase));
            }
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsWhenFolderPathDoesNotExist()
        {
            // act & assert
            ThrowsAsyncAssert.ThrowsAsync<InvalidOperationException>(SearchLogic.SearchWordsInFilesAsync(sampleExtensions, sampleSearchPhrase, notExistingFolderPath), Base.Properties.Resources.DirectoryDoesntExist);
        }

        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync_ThrowsErrorWhenThereAreNoMatchingFilesInPath()
        {
            ThrowsAsyncAssert.ThrowsAsync<EmptyFolderException>(SearchLogic.SearchWordsInFilesAsync(sampleExtensions, sampleSearchPhrase, existingFolderPathWithNoMatchingExtensions), Base.Properties.Resources.EmptyFolderException);
        }
    }
}