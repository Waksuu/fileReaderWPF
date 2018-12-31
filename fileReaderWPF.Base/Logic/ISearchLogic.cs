using fileReaderWPF.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Logic
{
    public interface ISearchLogic
    {
        Task<IEnumerable<PhraseLocation>> SearchWords(IEnumerable<string> filePaths, string phrase, object _syncLock);
    }
}
