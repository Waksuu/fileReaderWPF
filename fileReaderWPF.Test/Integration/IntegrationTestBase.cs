using fileReaderWPF.Base.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.Attributes;

namespace fileReaderWPF.Test.Real
{
    public class IntegrationTestBase
    {
        [Dependency]
        private Lazy<ISearchLogicService> SearchLogicServiceLazy { get; set; }

        private IUnityContainer _diContainer = ServiceLocator.MockReal;
        public ISearchLogicService SearchLogicService => SearchLogicServiceLazy.Value;

        [TestInitialize]
        public void BaseInit()
        {
            SearchLogicServiceLazy = _diContainer.Resolve<Lazy<ISearchLogicService>>();
        }
    }
}
