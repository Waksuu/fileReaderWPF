using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Logic.Helpers
{
    public class FileReaderPdfHelper : IFileReaderHelper
    {
        public IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern)
        {
            throw new NotImplementedException("Pdf");
        }
    }
}
