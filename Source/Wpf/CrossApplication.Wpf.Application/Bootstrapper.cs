using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CrossApplication.Core.Application;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Navigation;
using CrossApplication.Core.Net.Application.Modules;
using CrossApplication.Wpf.Application.Login;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.RegionAdapters;
using CrossApplication.Wpf.Contracts.Navigation;
using Fluent;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;
using Prism.Unity.Regions;

namespace CrossApplication.Wpf.Application
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void CreateShell()
        {
            _shell = ServiceLocator.Current.GetInstance<ApplicationShellView>();
            if (_shell == null)
                return;

            RegionManager.SetRegionManager(_shell, Container.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new AggregateModuleCatalog(base.CreateModuleCatalog(), new DirectoryModuleCatalog(@".\Modules"));
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

            Container.RegisterType<RichShellViewModel>();
            Container.RegisterType<object, RichShellView>(typeof(RichShellView).FullName);

            Container.RegisterType<LoginViewModel>();
            Container.RegisterType<object, LoginView>(typeof(LoginView).FullName);

            base.ConfigureContainer();
        }

        protected override void InitializeShell()
        {
            System.Windows.Application.Current.MainWindow = (Window) _shell;
            System.Windows.Application.Current.MainWindow.Show();

            Container.Resolve<IViewManager>().AddViewItem(new ViewItem("RichShellView", typeof(RichShellView), false, RegionNames.RichRegion));
            Container.Resolve<INavigationService>().NavigateTo("RichShellView");

            Container.Resolve<IViewManager>().LoginViewItem = new ViewItem("LoginView", typeof(LoginView), false, RegionNames.RichRegion);
        }

        private DependencyObject _shell;
    }
}