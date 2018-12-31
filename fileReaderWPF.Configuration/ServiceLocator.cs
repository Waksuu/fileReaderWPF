using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Repository;
using System;
using Unity;

namespace fileReaderWPF.Configuration
{
    public static class ServiceLocator
    {
        private static bool _initialized = false;

        private static Lazy<IUnityContainer> _wpfContainer;

        public static IUnityContainer WpfContainer
        {
            get
            {
                return _wpfContainer.Value;
            }
        }

        static ServiceLocator()
        {
            if (_initialized)
                return;

            _wpfContainer = new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();

                RegisterLogic(container);
                RegisterRepository(container);
                //RegisterPluginRepository(container);

                return container;
            });
        }

        private static void RegisterLogic(IUnityContainer container)
        {
            container.RegisterType<ISearchLogic, SearchLogic>();
        }

        private static void RegisterRepository(IUnityContainer container)
        {
            container.RegisterType<IFolderRepository, FolderRepository>();
        }
    }
}