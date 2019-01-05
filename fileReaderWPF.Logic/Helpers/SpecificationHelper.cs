using fileReaderWPF.Base.Patterns.Specification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace fileReaderWPF.Logic.Helpers
{
    public static class SpecificationHelper
    {
        public static ISpecification<string> SpecifyExtensions(IEnumerable<string> extensions)
        {
            if (extensions is null || !extensions.Any())
                throw new ArgumentNullException(Base.Properties.Resources.EmptyExtensions);

            ISpecification<string> extensionSpecification = new ExpressionSpecification<string>(o => Path.GetExtension(o).Equals(extensions.First()));

            foreach (var extension in extensions.Skip(1))
            {
                var specification = new ExpressionSpecification<string>(o => Path.GetExtension(o).Equals(extension));
                extensionSpecification = extensionSpecification.Or(specification);
            }
            return extensionSpecification;
        }
    }
}
