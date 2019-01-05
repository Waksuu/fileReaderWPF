using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using System.Collections.Generic;

namespace fileReaderWPF.Mock.Helpers
{
    public class MockFileReaderHelper : IFileReaderHelper
    {
        public const string SampleExistingPath = "Sample path";
        public const string SampleExisitingText = "Sample text";

        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern) => new List<PhraseLocation>()
            {
                new PhraseLocation() {  Paragraph = 2, Path = SampleExistingPath, Sentence = SampleExisitingText},
            };
    }
}