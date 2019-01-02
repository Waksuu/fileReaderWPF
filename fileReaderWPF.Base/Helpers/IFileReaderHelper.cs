using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Helpers
{
    public interface IFileReaderHelper
    {
        IEnumerable<PhraseLocation> GetPhraseLocationsFromFile(string filePath, string pattern);
    }
}
