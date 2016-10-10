using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Net.Application.Modules;
using CrossApplication.Core.Net.Application.Services;
using CrossApplication.Core.Net.Common.Container;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace CrossApplication.Core.Net.Application
{
    public abstract class BootstrapperBase : Core.Application.BootstrapperBase
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)base.CreateModuleCatalog();
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