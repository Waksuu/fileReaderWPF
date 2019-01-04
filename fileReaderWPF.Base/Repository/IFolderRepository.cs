using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Repository
{
    public interface IFolderRepository
    {
        /// <summary>
        /// Returns IEnumrable of file paths in a given folder with matching specification
        /// </summary>
        /// <param name="folderPath">Folder where the files will be searched in</param>
        /// <param name="specification">Specification of which files will be returned</param>
        /// <returns></returns>
        IEnumerable<string> GetFilesForPath(string folderPath, ISpecification<string> specification);
    }
}
