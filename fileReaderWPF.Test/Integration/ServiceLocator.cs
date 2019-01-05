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

        private static Lazy<IUnityContainer> _mockReal;

        public static IUnityContainer MockReal => _mockReal.Value;

        static ServiceLocator()
        {
            if (_initialized)
            {
                return;
            }

            _mockReal = new Lazy<IUnityContainer>(() =>
            {
                IUnityContainer container = new UnityContainer();

                RegisterRealLogic(container);
                RegisterRealRepository(container);

                return container;
            });
        }

        private static void RegisterRealLogic(IUnityContainer container)
        {
            container.RegisterType<ISearchLogicService, SearchLogicService>();

            container.RegisterType<IFileReaderHelper, TxtFileReaderHelper>(".txt");
            container.RegisterType<IFileReaderHelper, PdfFileReaderHelper>(".pdf");
            container.RegisterType<IFileReaderHelper, DocxFileReaderHelper>(".docx");

            container.RegisterType<IFileReaderHelperFactory, FileReaderHelperFactory>();
        }

        private static void RegisterRealRepository(IUnityContainer container) => container.RegisterType<IFolderRepository, FolderRepository>();
    }
}
