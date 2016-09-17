using System;
using System.Collections.Generic;
using CrossApplication.Wpf.Contracts;
using Prism.Regions;

namespace CrossApplication.Wpf.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void NavigateTo(string navigationKey)
        {
            _regionManager.RequestNavigate(_regions[navigationKey].Item1, _regions[navigationKey].Item2);
        }

        public void RegisterView<TView>(string navigationKey, string regionName)
        {
            _regions.Add(navigationKey, Tuple.Create(regionName, new Uri(typeof(TView).FullName, UriKind.Relative)));
        }

        private readonly IRegionManager _regionManager;
        private readonly Dictionary<string, Tuple<string, Uri>> _regions = new Dictionary<string, Tuple<string, Uri>>();
    }
}