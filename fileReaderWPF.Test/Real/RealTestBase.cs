using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Test.Real
{
    public class RealTestBase :TestBase
    {
        protected override IUnityContainer Container => ServiceLocator.MockReal;
    }
}
