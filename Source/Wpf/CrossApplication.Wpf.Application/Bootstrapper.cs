using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CrossApplication.Core.Application;
using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Application.Modules;
using CrossApplication.Core.Net.Common.Container;
using CrossApplication.Wpf.Application.Login;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.RegionAdapters;
using CrossApplication.Wpf.Contracts.Navigation;
using Fluent;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Regions.Behaviors;

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
            var moduleCatalog = (ModuleCatalog)base.CreateModuleCatalog();
            moduleCatalog.AddModuleInfo(new ModuleInfo { ModuleType = typeof(Common.Module), Tag = ModuleTags.Infrastructure });
            return new AggregateModuleCatalog(moduleCatalog, new DirectoryModuleCatalog(@".\Modules"));
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

        protected override IContainer CreateContainer()
        {
            var standardKernel = new StandardKernel();
            var container = new NinjectContainer(standardKernel);
            container.RegisterInstance<IServiceLocator>(new NinjectServiceLocator(standardKernel));
            return container;
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IRegionBehaviorFactory, RegionBehaviorFactory>(Lifetime.PerContainer);
            Container.RegisterType<IRegionManager, RegionManager>(Lifetime.PerContainer);
            Container.RegisterType<IRegionViewRegistry, RegionViewRegistry>(Lifetime.PerContainer);
            Container.RegisterType<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            Container.RegisterType<IRegionNavigationJournal, RegionNavigationJournal>();
            Container.RegisterType<IRegionNavigationService, RegionNavigationService>();
            Container.RegisterType<IRegionNavigationContentLoader, RegionNavigationContentLoader>(Lifetime.PerContainer);
            Container.RegisterType<RegionAdapterMappings, RegionAdapterMappings>(Lifetime.PerContainer);

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

            Container.Resolve<IViewManager>().AddViewItem(new ViewItem(typeof(RichShellView).FullName, false, RegionNames.RichRegion));
            var navigationService = Container.Resolve<INavigationService>();
            navigationService.NavigateTo(typeof(RichShellView).FullName);

            Container.Resolve<IViewManager>().LoginViewItem = new ViewItem(typeof(LoginView).FullName, false, RegionNames.RichRegion);
        }

        private DependencyObject _shell;
    }
}