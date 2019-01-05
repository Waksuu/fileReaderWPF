using fileReaderWPF.Base.Patterns.Specification;
using System.Collections.Generic;

namespace fileReaderWPF.Base.Repository
{
    public interface IFolderRepository
    {
        /// <summary>
        /// Returns IEnumrable of file paths in a given folder with matching specification
        /// </summary>
        /// <param name="folderPath">Folder where the files will be searched in</param>
        /// <param name="specification">Specification of which files will be returned</param>
        /// <returns>Returns IEnumerable<string> or null when directory does not exist</returns>
        IEnumerable<string> GetFilesForPath(string folderPath, ISpecification<string> specification);
    }
}
