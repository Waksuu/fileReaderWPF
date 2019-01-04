using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Mock.Repository
{
    public class FolderRepositoryMock : IFolderRepository
    {
        public IEnumerable<string> GetFilesForPath(string folderPath, ISpecification<string> specification)
        {
            return new List<string>()
            {
                "file.txt",
                "file2.txt",
                "document.docx",
                "document2.docx",
                "document3.docx",
            };
        }
    }
}
