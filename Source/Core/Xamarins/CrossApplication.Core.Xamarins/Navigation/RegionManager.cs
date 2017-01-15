using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using Microsoft.Practices.ServiceLocation;
using Prism.Common;
using Prism.Navigation;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(IApplicationProvider applicationProvider, IServiceLocator serviceLocator)
        {
            _applicationProvider = applicationProvider;
            _serviceLocator = serviceLocator;
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri)
        {
            await RequestNavigateAsync(regionName, uri, new NavigationParameters());
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            var currentPage = _applicationProvider.MainPage;
            if (currentPage == null)
            {
                _applicationProvider.MainPage = CreatePage(UriParsingHelper.GetUriSegments(uri).Dequeue());
                await RaiseActivatingView(_applicationProvider.MainPage, navigationParameters);
                await RaiseActivatedView(_applicationProvider.MainPage, navigationParameters);
                return;
            }

            if (!(currentPage is MasterDetailPage))
            {
                throw new NotImplementedException();
            }

            var activeView = ((MasterDetailPage) currentPage).Detail;
            await DeactivateActiveViews(activeView);
            ((MasterDetailPage) currentPage).Detail = CreatePage(UriParsingHelper.GetUriSegments(uri).Dequeue());
            await RaiseActivatingView(((MasterDetailPage) currentPage).Detail, navigationParameters);
            await RaiseActivatedView(((MasterDetailPage) currentPage).Detail, navigationParameters);
        }

        private static async Task RaiseActivatingView(BindableObject page, NavigationParameters navigationParameters)
        {
            var viewDeactivated = page.BindingContext as IViewActivatingAsync;
            if (viewDeactivated != null)
                await viewDeactivated.OnViewActivatingAsync(navigationParameters);
        }

        private static async Task RaiseActivatedView(BindableObject page, NavigationParameters navigationParameters)
        {
            var viewDeactivated = page?.BindingContext as IViewActivatedAsync;
            if (viewDeactivated != null)
                await viewDeactivated.OnViewActivatedAsync(navigationParameters);
        }

        private async Task DeactivateActiveViews(Page page)
        {
            var viewDeactivated = page.BindingContext as IViewDeactivatedAsync;
            if (viewDeactivated != null)
                await viewDeactivated.OnViewDeactivatedAsync();
        }

        private Page CreatePage(string segment)
        {
            return (Page) _serviceLocator.GetInstance<object>(UriParsingHelper.GetSegmentName(segment));
        }

        private readonly IApplicationProvider _applicationProvider;
        private readonly IServiceLocator _serviceLocator;
    }
}