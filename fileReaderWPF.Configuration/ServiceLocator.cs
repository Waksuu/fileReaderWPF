using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Logic.Helpers;
using fileReaderWPF.Repository;
using System;
using Unity;

namespace fileReaderWPF.Configuration
{
    public static class ServiceLocator
    {
        private static bool _initialized = false;
        private static Lazy<IUnityContainer> _wpfContainer;

        public static IUnityContainer WpfContainer => _wpfContainer.Value;

        static ServiceLocator()
        {
            if (_initialized)
            {
                return;
            }

            _wpfContainer = new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();

                RegisterLogic(container);
                RegisterRepository(container);

                return container;
            });
        }

        private static void RegisterLogic(IUnityContainer container)
        {
            container.RegisterType<ISearchLogicService, SearchLogicService>();

            container.RegisterType<IFileReaderHelper, TxtFileReaderHelper>(".txt");
            container.RegisterType<IFileReaderHelper, PdfFileReaderHelper>(".pdf");
            container.RegisterType<IFileReaderHelper, DocxFileReaderHelper>(".docx");

            container.RegisterType<IFileReaderHelperFactory, FileReaderHelperFactory>();
        }

        private static void RegisterRepository(IUnityContainer container) => container.RegisterType<IFolderRepository, FolderRepository>();
    }
}