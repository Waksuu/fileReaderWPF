using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fileReaderWPF.Repository
{
    public class FolderRepository : IFolderRepository
    {
        public IEnumerable<string> GetFilesForPath(string folderPath, ISpecification<string> specification)
        {
            return Directory.GetFiles(@folderPath).ToList().FindAll(o => specification.IsSatisfiedBy(o));
        }
    }
}
