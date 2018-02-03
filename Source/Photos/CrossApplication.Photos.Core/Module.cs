using System.Threading.Tasks;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Photos.Contracts;
using CrossApplication.Photos.Core.Navigation;
using CrossApplication.Photos.Core.Services;

namespace CrossApplication.Photos.Core
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
            _container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("Photos", ViewKeys.Shell, "ImageArea"));
            _container.RegisterType<IPhotoBackupService, PhotoBackupService>(Lifetime.PerContainer);
            return Task.CompletedTask;
        }

        public Task ActivateAsync()
        {
            return Task.CompletedTask;
        }
    }
}