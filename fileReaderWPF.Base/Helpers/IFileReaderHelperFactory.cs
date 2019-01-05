namespace fileReaderWPF.Base.Helpers
{
    public interface IFileReaderHelperFactory
    {
        /// <summary>
        /// Resolves implementation of interface IFileReaderHelper for specific extension
        /// </summary>
        /// <param name="extension">Determines which implementation of IFileReaderHelper to resolve</param>
        /// <returns>IFileReaderHelper implementation for specific extension</returns>
        IFileReaderHelper GetFileReaderHelper(string extension);
    }
}