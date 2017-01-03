using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;
using Prism.Navigation;
using INavigationService = Prism.Navigation.INavigationService;

namespace CrossApplication.Core.Xamarins.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri)
        {
            await _navigationService.NavigateAsync(uri);
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            await _navigationService.NavigateAsync(uri, navigationParameters);
        }

        private readonly INavigationService _navigationService;
    }
}