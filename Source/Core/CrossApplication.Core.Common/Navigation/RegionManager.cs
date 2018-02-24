using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;

namespace CrossApplication.Core.Common.Navigation
{
    public class RegionManager : IRegionManager
    {
        public RegionManager(IApplicationProvider applicationProvider)
        {
            _applicationProvider = applicationProvider;
        }

        public Task RequestNavigateAsync(string regionName, Uri uri)
        {
            return RequestNavigateAsync(regionName, uri, new NavigationParameters());
        }

        public async Task RequestNavigateAsync(string regionName, Uri uri, NavigationParameters navigationParameters)
        {
            await _applicationProvider.DeactivateActiveViewAsync();
            var view = _applicationProvider.CreateView(uri);

            if (regionName == "RichShell")
            {
                _applicationProvider.SetRichShell(view);
            }

            await _applicationProvider.ActivateViewAsync(view, navigationParameters);
        }

        private readonly IApplicationProvider _applicationProvider;
    }
}