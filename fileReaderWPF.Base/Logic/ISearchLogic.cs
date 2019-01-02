using fileReaderWPF.Base.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Base.Logic
{
    public interface ISearchLogic
    {
        Task<IEnumerable<PhraseLocation>> SearchWordsAsync(IEnumerable<string> filePaths, string phrase, IUnityContainer container);
    }
}
