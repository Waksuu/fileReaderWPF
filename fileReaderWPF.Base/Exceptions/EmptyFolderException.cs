using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Exceptions
{
    public class EmptyFolderException : Exception
    {
        public EmptyFolderException()
        {
        }

        public EmptyFolderException(string message) : base(message)
        {
        }
    }
}
