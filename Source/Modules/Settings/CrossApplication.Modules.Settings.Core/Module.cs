using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Modules.Settings.Core
{
    [Module]
    public class Module : IModule
    {
        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            // Replace url "Settings/Mail" with module unspecific url
            _container.RegisterInstance<IInfrastructureNavigationItem>(new MainNavigationItem("Settings", "Settings/Mail", "Settings"));

            return Task.CompletedTask;
        }

        public Task ActivateAsync()
        {
            return Task.CompletedTask;
        }

        private readonly IContainer _container;
    }
}