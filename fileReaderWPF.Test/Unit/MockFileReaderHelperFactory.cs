using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Mock.Helpers;

namespace fileReaderWPF.Test.Unit
{
    class MockFileReaderHelperFactory : IFileReaderHelperFactory
    {
        public IFileReaderHelper GetFileReaderHelper(string extension) => new MockFileReaderHelper();
    }
}
