using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;

namespace CrossApplication.Core.Wpf.Common.Application
{
    public class ApplicationProvider : IApplicationProvider
    {
        public ApplicationProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public async Task ActivateRichShell(object richShell, NavigationParameters navigationParameters)
        {
            await ActivateView(richShell, navigationParameters, System.Windows.Application.Current.MainWindow);
        }

        public object CreateView(Uri uri)
        {
            return _serviceLocator.GetInstance<object>(EnsureAbsolute(uri).AbsolutePath.TrimStart('/'));
        }

        public async Task ActivateViewAsync(object view, NavigationParameters navigationParameters)
        {
            await ActivateView(view, navigationParameters, (ContentControl)System.Windows.Application.Current.MainWindow.Content);
        }

        private static async Task ActivateView(object view, NavigationParameters navigationParameters, ContentControl contentControl)
        {
            if ((view as FrameworkElement)?.DataContext is IViewActivatingAsync viewActivating)
                await viewActivating.OnViewActivatingAsync(navigationParameters);

            contentControl.Content = view;

            if ((view as FrameworkElement)?.DataContext is IViewActivatedAsync viewActivated)
                await viewActivated.OnViewActivatedAsync(navigationParameters);
        }

        public async Task DeactivateActiveViewAsync()
        {
            if ((((ContentControl)System.Windows.Application.Current.MainWindow?.Content)?.Content as FrameworkElement)?.DataContext is IViewDeactivatedAsync viewDeactivated)
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