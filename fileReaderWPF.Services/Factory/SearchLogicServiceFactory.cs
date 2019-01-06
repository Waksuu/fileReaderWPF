using fileReaderWPF.Base.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Services.Factory
{
    public class SearchLogicServiceFactory
    {
        public static ISearchLogicService CreateSearchLogicService()
        {
            return new SearchLogicService();
        }
    }
}
