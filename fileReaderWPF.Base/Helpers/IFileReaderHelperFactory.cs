using fileReaderWPF.Base.Helpers;

namespace fileReaderWPF.Base.Helpers
{
    public interface IFileReaderHelperFactory
    {
        IFileReaderHelper GetFileReaderHelper(string extension);
    }
}