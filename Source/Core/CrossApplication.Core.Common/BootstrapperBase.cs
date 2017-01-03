using System.Threading.Tasks;
using CrossApplication.Core.Common.Container;
using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Common.Security;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Security;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Events;
using Prism.Logging;

namespace CrossApplication.Core.Common
{
    public abstract class BootstrapperBase
    {
        protected IContainer Container { get; private set; }

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
            ViewModelProvider.SetViewModelFactoryMethod(t => Container.Resolve(t));
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

        protected virtual IContainer CreateContainer()
        {
            var standardKernel = new StandardKernel();
            var container = new NinjectContainer(standardKernel);
            container.RegisterInstance<IServiceLocator>(new NinjectServiceLocator(standardKernel));
            return container;
        }

        protected virtual void ConfigureContainer()
        {
            Container.RegisterInstance(Container);
            Container.RegisterType<INavigationService, NavigationService>(Lifetime.PerContainer);
            Container.RegisterType<IEventAggregator, EventAggregator>(Lifetime.PerContainer);
            Container.RegisterType<IModuleManager, ModuleManager>(Lifetime.PerContainer);
            Container.RegisterType<IStringEncryption, StringEncryption>(Lifetime.PerContainer);
            Container.RegisterType<IViewManager, ViewManager>(Lifetime.PerContainer);
            Container.RegisterType<IUserManager, UserManager>(Lifetime.PerContainer);
            Container.RegisterInstance(_logger);
        }

        private ILoggerFacade _logger;
    }
}