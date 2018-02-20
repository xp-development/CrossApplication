using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CrossApplication.Core.Contracts.Views;
using Prism.Regions;
using IRegionManager = CrossApplication.Core.Contracts.Common.Navigation.IRegionManager;
using NavigationParameters = CrossApplication.Core.Contracts.Common.Navigation.NavigationParameters;

namespace CrossApplication.Core.Wpf.Common.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(Prism.Regions.IRegionManager regionManager, IRegionNavigationContentLoader regionNavigationContentLoader)
        {
            _regionManager = regionManager;
            _regionNavigationContentLoader = regionNavigationContentLoader;
        }

        public Task RequestNavigateAsync(string regionName, Uri uri)
        {
            return RequestNavigateAsync(regionName, uri, new NavigationParameters());
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            var navigationContext = new NavigationContext(null, uri);
            var region = _regionManager.Regions[regionName];
            await DeactivateActiveViews(region);
            var view = _regionNavigationContentLoader.LoadContent(region, navigationContext);
            await RaiseActivatingView(view, navigationParameters);
            region.Activate(view);
            await RaiseActivatedView(view, navigationParameters);
        }

        private static async Task RaiseActivatingView(object view, NavigationParameters navigationParameters)
        {
            if((view as FrameworkElement)?.DataContext is IViewActivatingAsync viewDeactivated)
                await viewDeactivated.OnViewActivatingAsync(navigationParameters);
        }

        private static async Task RaiseActivatedView(object view, NavigationParameters navigationParameters)
        {
            if((view as FrameworkElement)?.DataContext is IViewActivatedAsync viewDeactivated)
                await viewDeactivated.OnViewActivatedAsync(navigationParameters);
        }

        private static async Task DeactivateActiveViews(IRegion region)
        {
            foreach (var regionActiveView in region.ActiveViews.OfType<FrameworkElement>().Select(x => x.DataContext))
            {
                if (regionActiveView is IViewDeactivatedAsync viewDeactivated)
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
    }
}