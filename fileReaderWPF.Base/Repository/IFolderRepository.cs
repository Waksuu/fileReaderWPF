﻿using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Repository
{
    public interface IFolderRepository
    {
        IEnumerable<string> GetFilePaths(string folderPath, ISpecification<string> specification);
    }
}
