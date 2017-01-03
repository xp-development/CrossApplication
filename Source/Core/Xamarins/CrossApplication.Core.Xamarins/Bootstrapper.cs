using System;
using CrossApplication.Core.Common;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Core.Xamarins.About;
using CrossApplication.Core.Xamarins.Navigation;
using CrossApplication.Core.Xamarins.Shell;
using Microsoft.Practices.ServiceLocation;
using Prism.Common;
using Prism.Mvvm;
using Prism.Ninject.Navigation;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewModelFactory((o, type) => ((BindableObject)o)?.BindingContext);
            ViewModelProvider.SetViewEventCallback(ViewEventCallback);
            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((BindableObject) view).BindingContext = viewModel; });
        }

        private static void ViewEventCallback(object view, object viewModel)
        {
            var viewLoaded = viewModel as IViewLoadedAsync;
            if (viewLoaded != null)
                throw new NotSupportedException("IViewLoadedAsync is not supported.");

            var viewUnloaded = viewModel as IViewUnloadedAsync;
            if (viewUnloaded != null)
                throw new NotSupportedException("IViewUnloadedAsync is not supported.");

            var page = (Page) view;
            var viewLoading = viewModel as IViewLoadingAsync;
            if (viewLoading != null)
                page.Appearing += PageOnAppearing;

            var viewUnloading = viewModel as IViewUnloadingAsync;
            if (viewUnloading != null)
                page.Disappearing += PageOnDisappearing;
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IRegionManager, RegionManager>(Lifetime.PerContainer);
            Container.RegisterType<Prism.Navigation.INavigationService, NinjectPageNavigationService>(Lifetime.PerContainer);
            Container.RegisterType<IApplicationProvider, ApplicationProvider>(Lifetime.PerContainer);
            Container.RegisterType<IAboutService, AboutService>();

            base.ConfigureContainer();

            Container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("About", "About"));

            Container.RegisterType<object, AboutView>("About", Lifetime.PerContainer);
            var shellViewItem = new ViewItem("About", "");
            Container.Resolve<IViewManager>().AddViewItem(shellViewItem);
        }

        private static async void PageOnAppearing(object sender, EventArgs eventArgs)
        {
            var frameworkElement = (Page) sender;
            await ((IViewLoadingAsync) frameworkElement.BindingContext).OnViewLoadingAsync();
        }

        private static async void PageOnDisappearing(object sender, EventArgs eventArgs)
        {
            var frameworkElement = (Page) sender;
            await ((IViewUnloadingAsync) frameworkElement.BindingContext).OnViewUnloadingAsync();
        }

        protected override void CreateShell()
        {
            _shell = ServiceLocator.Current.GetInstance<RichShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainPage = _shell;
        }

        private RichShellView _shell;
    }
}