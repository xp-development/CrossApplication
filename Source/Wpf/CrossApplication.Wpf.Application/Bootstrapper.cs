using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Application.Modules;
using CrossApplication.Wpf.Application.Login;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using CrossApplication.Wpf.Common;
using CrossApplication.Wpf.Common.RegionAdapters;
using CrossApplication.Wpf.Common.ViewModels;
using CrossApplication.Wpf.Contracts.Navigation;
using Fluent;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using Prism.Regions.Behaviors;
using BootstrapperBase = CrossApplication.Core.Net.Application.BootstrapperBase;

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
            var aggregateModuleCatalog = (AggregateModuleCatalog)base.CreateModuleCatalog();
            aggregateModuleCatalog.ModuleCatalog.AddModuleInfo(new ModuleInfo { ModuleType = typeof(Module), Tag = ModuleTags.Infrastructure });
            return aggregateModuleCatalog;
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
            regionAdapterMappings.RegisterMapping(typeof(Backstage), Container.Resolve<BackstageTabControlAdapter>());
        }

        protected override void ConfigureViewModelLocator()
        {
            ViewModelProvider.SetViewModelFactoryMethod(t => Container.Resolve(t));
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

            RegisterViews();

            base.ConfigureContainer();
        }

        private void RegisterViews()
        {
            Container.RegisterType<RichShellViewModel>();
            Container.RegisterType<object, RichShellView>(typeof(RichShellView).FullName);

            Container.RegisterType<LoginViewModel>();
            Container.RegisterType<object, LoginView>(typeof(LoginView).FullName);

            Container.RegisterType<AboutViewModel>();
            Container.RegisterType<IBackstageTabViewModel, AboutView>(typeof(AboutView).FullName);
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