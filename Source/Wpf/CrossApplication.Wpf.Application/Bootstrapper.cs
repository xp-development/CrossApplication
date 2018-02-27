using System;
using System.Threading.Tasks;
using System.Windows;
using CrossApplication.Core.About;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Theming;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Net.Common;
using CrossApplication.Core.Net.Common.Modules;
using CrossApplication.Wpf.Application.Login;
using CrossApplication.Wpf.Application.Properties;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using Microsoft.Practices.ServiceLocation;
using Module = CrossApplication.Core.Wpf.Common.Module;

namespace CrossApplication.Wpf.Application
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void CreateShell()
        {
            _shell = ServiceLocator.Current.GetInstance<ApplicationShellView>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var aggregateModuleCatalog = (AggregateModuleCatalog) base.CreateModuleCatalog();
            aggregateModuleCatalog.ModuleCatalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(Module), Tag = ModuleTags.Infrastructure});
            return aggregateModuleCatalog;
        }

        protected override Task LoadThemeAsync()
        {
            ResourceDictionary myResourceDictionary;
            switch (Settings.Default.Theme)
            {
                case Theme.LightBlue:
                    myResourceDictionary = new ResourceDictionary {Source = new Uri("/CrossApplication.Core.Wpf.Themes;component/LightBlue/Colors.xaml", UriKind.Relative)};
                    break;
                default:
                    myResourceDictionary = new ResourceDictionary {Source = new Uri("/CrossApplication.Core.Wpf.Themes;component/LightBlue/Colors.xaml", UriKind.Relative)};
                    break;
            }

            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/MahApps.Metro;component/Styles/Controls.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/MahApps.Metro;component/Styles/Fonts.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/MahApps.Metro;component/Styles/Colors.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/MahApps.Metro;component/Styles/Accents/Blue.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/MahApps.Metro;component/Styles/Accents/BaseLight.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary {Source = new Uri("/CrossApplication.Core.Wpf.Themes;component/Generic/Generic.xaml", UriKind.Relative)});
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(myResourceDictionary);
            return base.LoadThemeAsync();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((FrameworkElement) view).DataContext = viewModel; });
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<Core.Contracts.Common.Navigation.IRegionManager, RegionManager>(Lifetime.PerContainer);
            RegisterViews();
            base.ConfigureContainer();
            Container.Resolve<IViewManager>().AddViewItem(new ViewItem("About", RegionNames.MainRegion, false, RegionNames.RichRegion));
        }

        private void RegisterViews()
        {
            Container.RegisterType<RichShellViewModel>(Lifetime.PerContainer);
            Container.RegisterType<object, RichShellView>(typeof(RichShellView).FullName, Lifetime.PerContainer);

            Container.RegisterType<LoginViewModel>();
            Container.RegisterType<object, LoginView>(typeof(LoginView).FullName);

            Container.RegisterType<AboutViewModel>(Lifetime.PerContainer);
            Container.RegisterType<object, AboutView>("About", Lifetime.PerContainer);
        }

        protected override void InitializeShell()
        {
            System.Windows.Application.Current.MainWindow = _shell;
            System.Windows.Application.Current.MainWindow.Show();

            var viewManager = Container.Resolve<IViewManager>();
            viewManager.RegisterRichShell(RegionNames.RichRegion, new ViewItem(typeof(RichShellView).FullName, RegionNames.RichRegion));
            viewManager.LoginViewItem = new ViewItem(typeof(LoginView).FullName, RegionNames.RichRegion);
        }

        private ApplicationShellView _shell;
    }
}