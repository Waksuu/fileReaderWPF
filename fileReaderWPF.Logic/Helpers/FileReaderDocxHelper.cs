using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xceed.Words.NET;

namespace fileReaderWPF.Logic.Helpers
{
    public class FileReaderDocxHelper : IFileReaderHelper
    {
        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern)
        {
            bool isMatch;
            int paragraphCount = 0;

            List<PhraseLocation> results = new List<PhraseLocation>();
            using (DocX document = DocX.Load(filePath))
            {
                foreach (var paragraph in document.Paragraphs)
                {
                    paragraphCount++;

                    isMatch = Regex.IsMatch(paragraph.Text, pattern);
                    if (isMatch)
                    {
                        lock (results)
                        {
                            results.Add(new PhraseLocation { Paragraph = paragraphCount, Path = filePath, Sentence = paragraph.Text });
                        }
                    }
                }
            }

            return results;
        }
    }
}