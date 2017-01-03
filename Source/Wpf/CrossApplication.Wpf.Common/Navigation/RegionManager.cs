using System;
using System.Threading.Tasks;
using Prism.Navigation;
using IRegionManager = CrossApplication.Core.Contracts.Common.Navigation.IRegionManager;

namespace CrossApplication.Wpf.Common.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(Prism.Regions.IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public Task RequestNavigateAsync(string regionName, Uri uri)
        {
            _regionManager.RequestNavigate(regionName, uri);
            return Task.FromResult(false);
        }

        public Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            var parameters = new Prism.Regions.NavigationParameters();
            foreach (var navigationParameter in navigationParameters)
            {
                parameters.Add(navigationParameter.Key, navigationParameter.Value);
            }

            _regionManager.RequestNavigate(regionName, uri, parameters);
            return Task.FromResult(false);
        }

        private readonly Prism.Regions.IRegionManager _regionManager;
    }
}