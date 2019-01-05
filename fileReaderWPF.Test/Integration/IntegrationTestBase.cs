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
        private Lazy<ISearchLogic> SearchLogicLazy { get; set; }

        private IUnityContainer _diContainer = ServiceLocator.MockReal;
        public ISearchLogic SearchLogic => SearchLogicLazy.Value;

        [TestInitialize]
        public void BaseInit()
        {
            SearchLogicLazy = _diContainer.Resolve<Lazy<ISearchLogic>>();
        }
    }
}
