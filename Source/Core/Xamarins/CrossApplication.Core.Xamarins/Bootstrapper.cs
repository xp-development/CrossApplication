using System;
using System.Globalization;
using System.Reflection;
using CrossApplication.Core.Common;
using CrossApplication.Core.Common.ViewModels;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Core.Xamarins.Shell;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelProvider.SetViewEventCallback(ViewEventCallback);
            ViewModelProvider.SetGetTypeCallback(viewType => Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewType.FullName, viewType.GetTypeInfo().Assembly.FullName), true));
            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((BindableObject)view).BindingContext = viewModel; });
        }

        private static void ViewEventCallback(object view, object viewModel)
        {
            var viewLoaded = viewModel as IViewLoadedAsync;
            if (viewLoaded != null)
                throw new NotSupportedException("IViewLoadedAsync is not supported.");

            var viewUnloaded = viewModel as IViewUnloadedAsync;
            if (viewUnloaded != null)
                throw new NotSupportedException("IViewUnloadedAsync is not supported.");

            var page = (Page)view;
            var viewLoading = viewModel as IViewLoadingAsync;
            if (viewLoading != null)
                page.Appearing += PageOnAppearing;

            var viewUnloading = viewModel as IViewUnloadingAsync;
            if (viewUnloading != null)
                page.Disappearing += PageOnDisappearing;
        }

        private static async void PageOnAppearing(object sender, EventArgs eventArgs)
        {
            var frameworkElement = (Page)sender;
            await((IViewLoadingAsync)frameworkElement.BindingContext).OnViewLoadingAsync();
        }

        private static async void PageOnDisappearing(object sender, EventArgs eventArgs)
        {
            var frameworkElement = (Page)sender;
            await((IViewUnloadingAsync)frameworkElement.BindingContext).OnViewUnloadingAsync();
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