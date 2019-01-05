using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace fileReaderWPF.Logic.Helpers
{
    public class PdfFileReaderHelper : IFileReaderHelper
    {
        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern)
        {
            bool isMatch;

            List<PhraseLocation> results = new List<PhraseLocation>();
            using (PdfReader pdfReader = new PdfReader(filePath))
            {
                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    string paragraph = PdfTextExtractor.GetTextFromPage(pdfReader, page);

                    isMatch = Regex.IsMatch(paragraph, pattern);
                    if (isMatch)
                    {
                        lock (results)
                        {
                            results.Add(new PhraseLocation { Paragraph = page, Path = filePath, Sentence = paragraph });
                        }
                    }
                }
            }

            return results;
        }
    }
}
