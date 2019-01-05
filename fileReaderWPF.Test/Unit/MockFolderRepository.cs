using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using System.Collections.Generic;

namespace fileReaderWPF.Mock.Repository
{
    public class MockFolderRepository : IFolderRepository
    {
        public IEnumerable<string> GetFilesForPath(string folderPath, ISpecification<string> specification) => new List<string>()
            {
                "file.txt",
                "file2.txt",
                "document.docx",
                "document2.docx",
                "document3.docx",
            };
    }
}
