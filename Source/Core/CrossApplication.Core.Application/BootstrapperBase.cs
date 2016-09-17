using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Logging;
using Prism.Unity;

namespace CrossApplication.Core.Application
{
    public abstract class BootstrapperBase
    {
        public IUnityContainer Container { get; set; }
        public ILoggerFacade Logger { get; set; }

        public void Run()
        {
            Logger = CreateLogger();
            ConfigureModuleCatalog();
            Container = CreateContainer();
            ConfigureContainer();
            ConfigureServiceLocator();
            ConfigureViewModelLocator();
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviors();
            RegisterFrameworkExceptionTypes();
            CreateShell();
            InitializeModules();
            InitializeShell();
        }

        protected virtual void ConfigureViewModelLocator()
        {
        }

        protected virtual ILoggerFacade CreateLogger()
        {
            return new DebugLogger();
        }

        protected abstract void CreateShell();

        protected virtual void InitializeModules()
        {
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

        protected virtual void ConfigureModuleCatalog()
        {
        }

        protected virtual void ConfigureServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => Container.Resolve<IServiceLocator>());
        }

        protected virtual IUnityContainer CreateContainer()
        {
            return new UnityContainer();
        }

        protected virtual void ConfigureContainer()
        {
            Container.RegisterType(typeof(IServiceLocator), typeof(UnityServiceLocatorAdapter), new ContainerControlledLifetimeManager());
            Container.RegisterInstance(Container, new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IEventAggregator), typeof(EventAggregator), new ContainerControlledLifetimeManager());
            Container.RegisterInstance(Logger);
        }
    }
}