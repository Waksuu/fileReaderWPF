using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Repository
{
    public class FolderRepository : IFolderRepository
    {
        public IEnumerable<string> GetFilePaths(string folderPath, ISpecification<string> specification)
        {
            var a = Directory.GetFiles(@folderPath).ToList();
            var b = a.FindAll(o => specification.IsSatisfiedBy(o));

            return b;
        }
    }
}
