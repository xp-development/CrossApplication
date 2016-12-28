using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using CrossApplication.Core.Common.ViewModels;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Theming;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Core.Net.Common;
using CrossApplication.Core.Net.Common.Modules;
using CrossApplication.Wpf.Application.Login;
using CrossApplication.Wpf.Application.Properties;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Common.RegionAdapters;
using CrossApplication.Wpf.Contracts.Navigation;
using Fluent;
using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using Prism.Regions.Behaviors;
using System.Reflection;
using Module = CrossApplication.Wpf.Common.Module;

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
            var aggregateModuleCatalog = (AggregateModuleCatalog) base.CreateModuleCatalog();
            aggregateModuleCatalog.ModuleCatalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(Module), Tag = ModuleTags.Infrastructure});
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

        protected override Task LoadThemeAsync()
        {
            ResourceDictionary myResourceDictionary;
            switch (Settings.Default.Theme)
            {
                case Theme.LightBlue:
                    myResourceDictionary = new ResourceDictionary {Source = new Uri("/CrossApplication.Wpf.Themes;component/LightBlue/Colors.xaml", UriKind.Relative)};
                    break;
                default:
                    myResourceDictionary = new ResourceDictionary {Source = new Uri("/CrossApplication.Wpf.Themes;component/LightBlue/Colors.xaml", UriKind.Relative)};
                    break;
            }

            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/Fluent;component/themes/office2013/generic.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/CrossApplication.Wpf.Themes;component/Generic/Generic.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            return base.LoadThemeAsync();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelProvider.SetViewEventCallback(ViewEventCallback);
            ViewModelProvider.SetGetTypeCallback(viewType => Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewType.FullName, viewType.GetTypeInfo().Assembly.FullName), name => viewType.GetTypeInfo().Assembly, null, true));
            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((FrameworkElement) view).DataContext = viewModel; });
        }

        private static void ViewEventCallback(object view, object viewModel)
        {
            var viewLoading = viewModel as IViewLoadingAsync;
            if (viewLoading != null)
                throw new NotSupportedException("IViewLoadingAsync is not supported.");

            var viewUnloading = viewModel as IViewUnloadingAsync;
            if (viewUnloading != null)
                throw new NotSupportedException("IViewUnloadingAsync is not supported.");

            var viewLoaded = viewModel as IViewLoadedAsync;
            if (viewLoaded != null)
            {
                var frameworkElement = (FrameworkElement) view;
                frameworkElement.Loaded += FrameworkElementOnLoaded;
                frameworkElement.Unloaded += FrameworkElementOnUnloaded;
            }

            var viewUnloaded = viewModel as IViewUnloadedAsync;
            if (viewUnloaded != null)
            {
                var frameworkElement = (FrameworkElement)view;
                frameworkElement.Unloaded += FrameworkElementOnUnloaded;
            }
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

            var viewManager = Container.Resolve<IViewManager>();
            viewManager.RichViewItem = new ViewItem(typeof(RichShellView).FullName, RegionNames.RichRegion);
            viewManager.LoginViewItem = new ViewItem(typeof(LoginView).FullName, RegionNames.RichRegion);
        }

        private static async void FrameworkElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var frameworkElement = (FrameworkElement) sender;
            await ((IViewLoadedAsync) frameworkElement.DataContext).OnViewLoadedAsync();
        }

        private static async void FrameworkElementOnUnloaded(object sender, RoutedEventArgs e)
        {
            var frameworkElement = (FrameworkElement) sender;
            frameworkElement.Loaded -= FrameworkElementOnLoaded;
            frameworkElement.Unloaded -= FrameworkElementOnUnloaded;

            var viewLoaded = frameworkElement.DataContext as IViewUnloadedAsync;
            if (viewLoaded != null)
            {
                await ((IViewUnloadedAsync) frameworkElement.DataContext).OnViewUnloadedAsync();
            }
        }

        private DependencyObject _shell;
    }
}