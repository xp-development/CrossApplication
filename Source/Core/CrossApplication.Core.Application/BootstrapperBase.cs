using System.Threading.Tasks;
using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Logging;

namespace CrossApplication.Core.Application
{
    public abstract class BootstrapperBase
    {
        protected IContainer Container { get; private set; }
        protected ILoggerFacade Logger { get; private set; }

        public async Task Run()
        {
            Logger = CreateLogger();

            Container = CreateContainer();
            ConfigureContainer();
            ConfigureServiceLocator();
            ConfigureViewModelLocator();
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviors();
            RegisterFrameworkExceptionTypes();
            CreateShell();

            var moduleManager = Container.Resolve<IModuleManager>();
            moduleManager.SetModuleCatalog(CreateModuleCatalog());

            await InitializeInfrastructureModulesAsync(moduleManager);
            await InitializeModulesAsync(moduleManager);
            InitializeShell();
            await ActivateInfrastructureModulesAsync(moduleManager);
            await ActivateModulesAsync(moduleManager);
        }

        protected virtual void ConfigureViewModelLocator()
        {
        }

        protected virtual ILoggerFacade CreateLogger()
        {
            return new DebugLogger();
        }

        protected abstract void CreateShell();

        private Task InitializeInfrastructureModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.InizializeAsync(ModuleTags.Infrastructure);
        }

        private Task InitializeModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.InizializeAsync(ModuleTags.DefaultModule);
        }

        private Task ActivateInfrastructureModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.ActivateAsync(ModuleTags.Infrastructure);
        }

        private Task ActivateModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.ActivateAsync(ModuleTags.DefaultModule);
        }

        protected virtual void InitializeShell()
        {
        }

        protected virtual void RegisterFrameworkExceptionTypes()
        {
        }

        protected virtual void ConfigureDefaultRegionBehaviors()
        {
        }

        protected virtual void ConfigureRegionAdapterMappings()
        {
        }

        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            return new ModuleCatalog();
        }

        protected virtual void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
        }

        protected abstract IContainer CreateContainer();

        protected virtual void ConfigureContainer()
        {
            Container.RegisterInstance(Container);
            Container.RegisterType<IEventAggregator, EventAggregator>(Lifetime.PerContainer);
            Container.RegisterType<IModuleManager, ModuleManager>(Lifetime.PerContainer);
            Container.RegisterInstance(Logger);
        }
    }
}