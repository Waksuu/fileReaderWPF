using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace fileReaderWPF.Test.Real.Logic
{
    [TestClass]
    public class SearchLogicTest : RealTestBase
    {
        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync()
        {
            var extensions = new List<string>()
            {
                ".txt",
                ".pdf",
                ".docx",
            };
            string phrase = "is always the";
            string folderPath = @"./TestResources";

            var result = SearchLogic.SearchWordsInFilesAsync(extensions, phrase, folderPath).Result.ToList();

            foreach (var item in result)
            {
                Assert.IsTrue(item.Sentence.Contains(phrase));
            }
        }
    }
}