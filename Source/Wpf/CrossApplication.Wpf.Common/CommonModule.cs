using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace CrossApplication.Wpf.Common
{
    [Module(ModuleName = "CrossApplication.Core.Wpf.Common")]
    public class CommonModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public CommonModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
        }
    }
}