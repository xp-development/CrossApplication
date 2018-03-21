using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins.Common.Application
{
    public class ApplicationProvider : IApplicationProvider
    {
        public ApplicationProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public async Task ActivateRichShell(object richShell, NavigationParameters navigationParameters)
        {
            if (Xamarin.Forms.Application.Current.MainPage?.BindingContext is IViewActivatingAsync viewActivating)
                await viewActivating.OnViewActivatingAsync(navigationParameters);

            Xamarin.Forms.Application.Current.MainPage = (Page)richShell;

            if (Xamarin.Forms.Application.Current.MainPage.BindingContext is IViewActivatedAsync viewActivated)
                await viewActivated.OnViewActivatedAsync(navigationParameters);
        }

        public object CreateView(Uri uri)
        {
            return _serviceLocator.GetInstance<object>(EnsureAbsolute(uri).AbsolutePath.TrimStart('/'));
        }

        public async Task ActivateViewAsync(object view, NavigationParameters navigationParameters)
        {
            await ActivateView(view, navigationParameters, Xamarin.Forms.Application.Current.MainPage);
        }

        private static async Task ActivateView(object view, NavigationParameters navigationParameters, Page richPage)
        {
            if (richPage?.BindingContext is IViewActivatingAsync viewActivating)
                await viewActivating.OnViewActivatingAsync(navigationParameters);

            if (richPage is MasterDetailPage page)
                page.Detail = (Page) view;

            if (((Page)view).BindingContext is IViewActivatedAsync viewActivated)
                await viewActivated.OnViewActivatedAsync(navigationParameters);
        }

        public async Task DeactivateActiveViewAsync()
        {
            if ((Xamarin.Forms.Application.Current.MainPage as ContentPage)?.Content?.BindingContext is IViewDeactivatedAsync viewDeactivated)
                await viewDeactivated.OnViewDeactivatedAsync();
        }

        private static Uri EnsureAbsolute(Uri uri)
        {
            if (uri.IsAbsoluteUri)
                return uri;

            return !uri.OriginalString.StartsWith("/", StringComparison.Ordinal) ? new Uri("http://localhost/" + uri, UriKind.Absolute) : new Uri("http://localhost" + uri, UriKind.Absolute);
        }

        private readonly IServiceLocator _serviceLocator;
    }
}