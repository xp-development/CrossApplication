using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Xamarins.About;
using Prism.Common;
using Prism.Mvvm;
using Prism.Ninject.Navigation;
using Xamarin.Forms;
using INavigationService = Prism.Navigation.INavigationService;

namespace CrossApplication.Core.Xamarins
{
    public abstract class BootstrapperBase : Common.BootstrapperBase
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory((o, type) => ((BindableObject) o)?.BindingContext);
            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((BindableObject) view).BindingContext = viewModel; });
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IRegionManager, Common.Navigation.RegionManager>(Lifetime.PerContainer);
            Container.RegisterType<INavigationService, NinjectPageNavigationService>(Lifetime.PerContainer);
            Container.RegisterType<IApplicationProvider, ApplicationProvider>(Lifetime.PerContainer);
            Container.RegisterType<IAboutService, AboutService>();

            base.ConfigureContainer();

            Container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("About", "About", "Help"));

            Container.RegisterType<object, AboutView>("About", Lifetime.PerContainer);
            var shellViewItem = new ViewItem("About", "");
            Container.Resolve<IViewManager>().AddViewItem(shellViewItem);
        }

        protected override void CreateShell()
        {
//            _shell = ServiceLocator.Current.GetInstance<RichShellView>();
        }

        protected override void InitializeShell()
        {
//            Application.Current.MainPage = _shell;
        }

//        private RichShellView _shell;
    }
}