using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileReaderWPF.Base.Helpers
{
    public interface ISpecificationHelper
    {
        ISpecification<string> SpecifyExtensions(IEnumerable<string> extensions);
    }
}
