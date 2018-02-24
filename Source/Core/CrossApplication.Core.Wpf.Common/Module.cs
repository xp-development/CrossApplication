using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Wpf.Common.Application;

namespace CrossApplication.Core.Wpf.Common
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        private readonly IContainer _container;

        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<IApplicationProvider, ApplicationProvider>(Lifetime.PerContainer);

            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}