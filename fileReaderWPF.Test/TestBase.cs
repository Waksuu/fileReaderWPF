using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unity;
using Unity.Attributes;

namespace fileReaderWPF.Test
{
    public abstract class TestBase
    {
        protected abstract IUnityContainer Container { get; }

        [Dependency]
        private Lazy<ISearchLogicService> SearchLogicLazy { get; set; }
        public ISearchLogicService SearchLogic => SearchLogicLazy.Value;

        [TestInitialize]
        public virtual void BaseInit()
        {
            SearchLogicLazy = Container.Resolve<Lazy<ISearchLogicService>>();
        }
    }
}