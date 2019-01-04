using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Logic.Helpers;
using fileReaderWPF.Mock.Helpers;
using fileReaderWPF.Mock.Repository;
using fileReaderWPF.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace fileReaderWPF.Test
{
    public static class ServiceLocator
    {
        private static bool _initialized = false;

        private static Lazy<IUnityContainer> _mock;
        private static Lazy<IUnityContainer> _mockReal;

        public static IUnityContainer Mock => _mock.Value;

        public static IUnityContainer MockReal => _mockReal.Value;

        static ServiceLocator()
        {
            if (_initialized)
            {
                return;
            }

            _mock = new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();

                RegisterLogic(container);
                RegisterRepository(container);

                return container;
            });

            _mockReal = new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();

                RegisterRealLogic(container);
                RegisterRealRepository(container);

                return container;
            });
        }

        private static void RegisterLogic(IUnityContainer container)
        {
            container.RegisterType<ISearchLogicService, SearchLogicService>();

            container.RegisterType<IFileReaderHelper, FileReaderHelperMock>(".txt");
            container.RegisterType<IFileReaderHelper, FileReaderHelperMock>(".docx");
            container.RegisterType<IFileReaderHelper, FileReaderHelperMock>(".pdf");
        }

        private static void RegisterRepository(IUnityContainer container) => container.RegisterType<IFolderRepository, FolderRepositoryMock>();

        private static void RegisterRealLogic(IUnityContainer container)
        {
            container.RegisterType<ISearchLogicService, SearchLogicService>();

            container.RegisterType<IFileReaderHelper, FileReaderTxtHelper>(".txt");
            container.RegisterType<IFileReaderHelper, FileReaderPdfHelper>(".pdf");
            container.RegisterType<IFileReaderHelper, FileReaderDocxHelper>(".docx");
        }

        private static void RegisterRealRepository(IUnityContainer container) => container.RegisterType<IFolderRepository, FolderRepository>();
    }
}
