using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CommonServiceLocator;
using CrossApplication.Core.Contracts.Views;
using Prism.Regions;
using IRegionManager = CrossApplication.Core.Contracts.Common.Navigation.IRegionManager;
using NavigationParameters = Prism.Navigation.NavigationParameters;

namespace CrossApplication.Core.Wpf.Common.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(Prism.Regions.IRegionManager regionManager, IRegionNavigationContentLoader regionNavigationContentLoader, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _regionNavigationContentLoader = regionNavigationContentLoader;
            _serviceLocator = serviceLocator;
        }

        public Task RequestNavigateAsync(string regionName, Uri uri)
        {
            return RequestNavigateAsync(regionName, uri, new NavigationParameters());
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            var parameters = new Prism.Regions.NavigationParameters();
            foreach (var navigationParameter in navigationParameters)
            {
                parameters.Add(navigationParameter.Key, navigationParameter.Value);
            }

            var navigationContext = new NavigationContext(null, uri);
            var region = _regionManager.Regions[regionName];
            await DeactivateActiveViews(region);
            var view = _regionNavigationContentLoader.LoadContent(region, navigationContext);
            await RaiseActivatingView(view, navigationParameters);
            region.Activate(view);
            var journalEntry = _serviceLocator.GetInstance<IRegionNavigationJournalEntry>();
            journalEntry.Uri = navigationContext.Uri;
            journalEntry.Parameters = navigationContext.Parameters;
            _serviceLocator.GetInstance<IRegionNavigationJournal>().RecordNavigation(journalEntry);
            await RaiseActivatedView(view, navigationParameters);
        }

        private static async Task RaiseActivatingView(object view, NavigationParameters navigationParameters)
        {
            var viewDeactivated = (view as FrameworkElement)?.DataContext as IViewActivatingAsync;
            if(viewDeactivated != null)
                await viewDeactivated.OnViewActivatingAsync(navigationParameters);
        }

        private static async Task RaiseActivatedView(object view, NavigationParameters navigationParameters)
        {
            var viewDeactivated = (view as FrameworkElement)?.DataContext as IViewActivatedAsync;
            if(viewDeactivated != null)
                await viewDeactivated.OnViewActivatedAsync(navigationParameters);
        }

        private static async Task DeactivateActiveViews(IRegion region)
        {
            foreach (var regionActiveView in region.ActiveViews.OfType<FrameworkElement>().Select(x => x.DataContext))
            {
                var viewDeactivated = regionActiveView as IViewDeactivatedAsync;
                if (viewDeactivated != null)
                    await viewDeactivated.OnViewDeactivatedAsync();
            }

            var views = region.ActiveViews.ToArray();
            foreach (var view in views)
            {
                region.Deactivate(view);
                region.Remove(view);
            }
        }

        private readonly Prism.Regions.IRegionManager _regionManager;
        private readonly IRegionNavigationContentLoader _regionNavigationContentLoader;
        private readonly IServiceLocator _serviceLocator;
    }
}