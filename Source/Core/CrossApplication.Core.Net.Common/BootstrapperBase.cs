using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Core.Net.Common.Modules;
using CrossApplication.Core.Net.Common.Services;
using CrossApplication.Core.Net.Common.Storage;

namespace CrossApplication.Core.Net.Common
{
    public abstract class BootstrapperBase : Core.Common.BootstrapperBase
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog) base.CreateModuleCatalog();
            return new AggregateModuleCatalog(moduleCatalog, new DirectoryModuleCatalog(@".\Modules"));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IStorage, LocalStorage>(Lifetime.PerContainer);
            Container.RegisterType<IAboutService, AboutService>();
        }
    }
}