using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Mock.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Test.Unit
{
    class MockFileReaderHelperFactory : IFileReaderHelperFactory
    {
        public IFileReaderHelper GetFileReaderHelper(string extension) => new FileReaderHelperMock();
    }
}
