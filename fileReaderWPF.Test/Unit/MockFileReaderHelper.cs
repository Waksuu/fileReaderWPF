using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using System.Collections.Generic;

namespace fileReaderWPF.Mock.Helpers
{
    public class FileReaderHelperMock : IFileReaderHelper
    {
        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern) => new List<PhraseLocation>()
            {
                new PhraseLocation() {  Paragraph = 2, Path = "Sample path", Sentence = "Sample text"},
            };
    }
}