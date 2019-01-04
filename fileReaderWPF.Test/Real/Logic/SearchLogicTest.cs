﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            List<string> extensions = new List<string>()
            {
                ".txt",
                ".pdf",
                ".docx",
            };
            string phrase = "is always the";
            string folderPath = @"Z:\testReader";

            var result = SearchLogic.SearchWordsInFilesAsync(extensions, phrase, folderPath, Container).Result.ToList();

            foreach (var item in result)
            {
                Assert.IsTrue(item.Sentence.Contains(phrase));
            }
        }
    }
}