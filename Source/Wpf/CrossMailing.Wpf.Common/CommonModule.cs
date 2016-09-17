using CrossMailing.Wpf.Common.Navigation;
using Microsoft.Practices.Unity;
using Prism.Modularity;

namespace CrossMailing.Wpf.Common
{
    [Module(ModuleName = "CrossMailing.Wpf.Common")]
    public class CommonModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public CommonModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<Contracts.INavigationService, NavigationService>(new ContainerControlledLifetimeManager());
        }
    }
}