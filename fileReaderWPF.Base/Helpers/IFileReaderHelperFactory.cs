namespace fileReaderWPF.Base.Helpers
{
    public interface IFileReaderHelperFactory
    {
        IFileReaderHelper GetFileReaderHelper(string extension);
    }
}