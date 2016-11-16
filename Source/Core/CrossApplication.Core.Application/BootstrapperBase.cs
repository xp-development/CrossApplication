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
        private ILoggerFacade _logger;

        public async Task Run()
        {
            _logger = CreateLogger();

            Container = CreateContainer();
            ConfigureContainer();
            ConfigureServiceLocator();
            ConfigureViewModelLocator();
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviors();
            RegisterFrameworkExceptionTypes();
            await LoadThemeAsync();
            CreateShell();

            Container.RegisterInstance(CreateModuleCatalog());

            var moduleManager = Container.Resolve<IModuleManager>();
            await InitializeInfrastructureModulesAsync(moduleManager);
            await InitializeModulesAsync(moduleManager);
            InitializeShell();
            await ActivateInfrastructureModulesAsync(moduleManager);
            await ActivateModulesAsync(moduleManager);
        }

        protected virtual Task LoadThemeAsync()
        {
            return Task.FromResult(false);
        }

        protected virtual void ConfigureViewModelLocator()
        {
        }

        private ILoggerFacade CreateLogger()
        {
            return new DebugLogger();
        }

        protected abstract void CreateShell();

        private static Task InitializeInfrastructureModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.InizializeAsync(ModuleTags.Infrastructure);
        }

        private static Task InitializeModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.InizializeAsync(ModuleTags.DefaultModule);
        }

        private static Task ActivateInfrastructureModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.ActivateAsync(ModuleTags.Infrastructure);
        }

        private static Task ActivateModulesAsync(IModuleManager moduleManager)
        {
            return moduleManager.ActivateAsync(ModuleTags.DefaultModule);
        }

        protected virtual void InitializeShell()
        {
        }

        private void RegisterFrameworkExceptionTypes()
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

        private void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
        }

        protected abstract IContainer CreateContainer();

        protected virtual void ConfigureContainer()
        {
            Container.RegisterInstance(Container);
            Container.RegisterType<IEventAggregator, EventAggregator>(Lifetime.PerContainer);
            Container.RegisterType<IModuleManager, ModuleManager>(Lifetime.PerContainer);
            Container.RegisterInstance(_logger);
        }
    }
}