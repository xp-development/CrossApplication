using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Net.Application.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Mail.Core.Navigation;
using Microsoft.Practices.Unity;

namespace CrossApplication.Mail.Core.Net
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        public Module(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public Task InitializeAsync()
        {
            _unityContainer.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("E-Mail", ViewKeys.Shell));
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }

        private readonly IUnityContainer _unityContainer;
    }
}