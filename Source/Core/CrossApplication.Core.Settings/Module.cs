using System;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Core.Settings
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
            _container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("Settings", ViewKeys.Shell, "Settings"));
        }

        public Task ActivateAsync()
        {
            throw new NotImplementedException();
        }

        private readonly IContainer _container;
    }
}