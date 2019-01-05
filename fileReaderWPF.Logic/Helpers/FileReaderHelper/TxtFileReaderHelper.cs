using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace fileReaderWPF.Logic.Helpers
{
    public class TxtFileReaderHelper : IFileReaderHelper
    {
        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern)
        {
            string paragraph;
            bool isMatch;
            int paragraphCount = 0;

            List<PhraseLocation> results = new List<PhraseLocation>();

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                while ((paragraph = streamReader.ReadLine()) != null)
                {
                    paragraphCount++;

                    isMatch = Regex.IsMatch(paragraph, pattern);
                    if (isMatch)
                    {
                        lock (results)
                        {
                            results.Add(new PhraseLocation { Paragraph = paragraphCount, Path = filePath, Sentence = paragraph });
                        }
                    }
                }
            }

            return results;
        }
    }
}
