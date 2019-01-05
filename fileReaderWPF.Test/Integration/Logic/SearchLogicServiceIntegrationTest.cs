using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
        private static readonly string existingFolderPath = @"./TestResources";

        [TestMethod]
        public void SearchLogicServiceTest_SearchWordsInFilesAsync()
        {
            // act
            var result = SearchLogicService.SearchWordsInFilesAsync(sampleExtensions, sampleSearchPhrase, existingFolderPath).Result.ToList();

            // assert
            foreach (var item in result)
            {
                Assert.IsTrue(item.Sentence.Contains(sampleSearchPhrase));
            }
        }
    }
}