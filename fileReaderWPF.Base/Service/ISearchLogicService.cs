using fileReaderWPF.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Service
{
    public interface ISearchLogicService
    {
       Task<IEnumerable<PhraseLocation>> FindSentencesInFolderPath(IEnumerable<string> extensions, string phrase, string folderPath);
    }
}
