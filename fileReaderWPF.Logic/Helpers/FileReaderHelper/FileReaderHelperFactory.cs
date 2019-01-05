using fileReaderWPF.Base.Helpers;
using Unity;

namespace fileReaderWPF.Logic.Helpers
{
    public class FileReaderHelperFactory : IFileReaderHelperFactory
    {
        private readonly IUnityContainer _unityContainer;

        public FileReaderHelperFactory(IUnityContainer unityContainer) => _unityContainer = unityContainer;

        public IFileReaderHelper GetFileReaderHelper(string extension) => _unityContainer.Resolve<IFileReaderHelper>(extension);
    }
}
