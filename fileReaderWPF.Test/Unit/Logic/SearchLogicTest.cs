using fileReaderWPF.Base.Repository;
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
    public class SearchLogicTest : UnitTestBase
    {
        [TestMethod]
        public void SearchLogicTest_SearchWordsInFilesAsync()
        {
            List<string> extensions = new List<string>()
            {
                ".txt",
                ".docx",
            };
            string phrase = "doesn't matter";
            string folderPath = @"doesn't matter";

            var result = SearchLogic.SearchWordsInFilesAsync(extensions, phrase, folderPath).Result.ToList();

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("Sample text", result[0].Sentence);
            Assert.AreEqual("Sample path", result[0].Path);
        }
    }
}
