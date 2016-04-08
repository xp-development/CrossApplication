using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CrossMailing.Application;
using CrossMailing.Common;
using CrossMailing.Wpf.Application.Shell;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Common.RegionAdapters;
using Fluent;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Unity.Regions;

namespace CrossMailing.Wpf.Application
{
    public class Bootstrapper : BootstrapperBase
    {
        public DependencyObject Shell { get; set; }

        protected override void RunShell()
        {
            Shell = CreateShell();
            if (Shell == null)
                return;

            RegionManager.SetRegionManager(Shell, Container.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
            InitializeShell();
        }

        protected override void InitializeModules()
        {
            Container.Resolve<IModuleManager>().Run();

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ActivateModuleEvent>().Publish(new ActivateModulePayload(UniqueIdentifier.MailModuleIdentifier));
        }

        protected override void ConfigureModuleCatalog()
        {
            _moduleCatalog = new DirectoryModuleCatalog {ModulePath = @".\Modules"};
        }

        protected override void ConfigureDefaultRegionBehaviors()
        {
            var instance = ServiceLocator.Current.GetInstance<IRegionBehaviorFactory>();
            instance.AddIfMissing("ContextToDependencyObject", typeof(BindRegionContextToDependencyObjectBehavior));
            instance.AddIfMissing("ActiveAware", typeof(RegionActiveAwareBehavior));
            instance.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey, typeof(SyncRegionContextWithHostBehavior));
            instance.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));
            instance.AddIfMissing("RegionMemberLifetimeBehavior", typeof(RegionMemberLifetimeBehavior));
            instance.AddIfMissing("ClearChildViews", typeof(ClearChildViewsRegionBehavior));
            instance.AddIfMissing("AutoPopulate", typeof(AutoPopulateRegionBehavior));
        }

        protected override void ConfigureRegionAdapterMappings()
        {
            var regionAdapterMappings = ServiceLocator.Current.GetInstance<RegionAdapterMappings>();

            regionAdapterMappings.RegisterMapping(typeof(Selector), ServiceLocator.Current.GetInstance<SelectorRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(ItemsControl), ServiceLocator.Current.GetInstance<ItemsControlRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(ContentControl), ServiceLocator.Current.GetInstance<ContentControlRegionAdapter>());
            regionAdapterMappings.RegisterMapping(typeof(Ribbon), Container.Resolve<RibbonRegionAdapter>());
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(t => Container.Resolve(t));
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType => Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewType.FullName, viewType.GetTypeInfo().Assembly.FullName)));
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType(typeof(IRegionBehaviorFactory), typeof(RegionBehaviorFactory), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IRegionManager), typeof(RegionManager), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IRegionBehaviorFactory), typeof(RegionBehaviorFactory), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IRegionViewRegistry), typeof(RegionViewRegistry), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IRegionNavigationJournalEntry), typeof(RegionNavigationJournalEntry));
            Container.RegisterType(typeof(IRegionNavigationJournal), typeof(RegionNavigationJournal));
            Container.RegisterType(typeof(IRegionNavigationService), typeof(RegionNavigationService));
            Container.RegisterType(typeof(IRegionNavigationContentLoader), typeof(UnityRegionNavigationContentLoader), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(RegionAdapterMappings), typeof(RegionAdapterMappings), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IModuleInitializer), typeof(ModuleInitializer), new ContainerControlledLifetimeManager());
            Container.RegisterType(typeof(IModuleManager), typeof(ModuleManager), new ContainerControlledLifetimeManager());
            Container.RegisterInstance(_moduleCatalog, new ContainerControlledLifetimeManager());
            Container.RegisterType<RichShellViewModel>();

            base.ConfigureContainer();
        }

        private static DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<RichShellView>();
        }

        private void InitializeShell()
        {
            System.Windows.Application.Current.MainWindow = (Window) Shell;
            System.Windows.Application.Current.MainWindow.Show();
        }

        private IModuleCatalog _moduleCatalog;
    }
}