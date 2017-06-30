using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Authorization;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;

namespace CrossApplication.Core.AuthProviders.Google
{
    [Module]
    public class Module : IModule
    {
        private readonly IContainer _container;

        public Module(IContainer container)
        {
            _container = container;
        }

        public Task InitializeAsync()
        {
            _container.RegisterType<IAuthorizationProvider, GoogleAuthProvider>();

            return Task.CompletedTask;
        }

        public Task ActivateAsync()
        {
            return Task.CompletedTask;
        }
    }
}