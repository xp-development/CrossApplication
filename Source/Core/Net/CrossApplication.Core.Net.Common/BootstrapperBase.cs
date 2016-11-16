using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Net.Common.Container;
using CrossApplication.Core.Net.Common.Modules;
using CrossApplication.Core.Net.Common.Services;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace CrossApplication.Core.Net.Common
{
    public abstract class BootstrapperBase : Core.Common.BootstrapperBase
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog) base.CreateModuleCatalog();
            return new AggregateModuleCatalog(moduleCatalog, new DirectoryModuleCatalog(@".\Modules"));
        }

        protected override IContainer CreateContainer()
        {
            var standardKernel = new StandardKernel();
            var container = new NinjectContainer(standardKernel);
            container.RegisterInstance<IServiceLocator>(new NinjectServiceLocator(standardKernel));
            container.RegisterType<IAboutService, AboutService>();
            return container;
        }
    }
}