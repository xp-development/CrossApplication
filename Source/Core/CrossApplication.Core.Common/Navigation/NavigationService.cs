using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using Prism.Navigation;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Core.Common.Navigation
{
    public class NavigationService : INavigationService
    {
        public NavigationService(IRegionManager regionManager, IViewManager viewManager, IUserManager userManager)
        {
            _regionManager = regionManager;
            _viewManager = viewManager;
            _userManager = userManager;
        }

        public async Task NavigateToAsync(string navigationKey)
        {
            var viewItem = _viewManager.GetViewItem(navigationKey);
            await NavigateTo(viewItem);
            _lastViewItem = viewItem;
        }

        public async Task NavigateBackAsync()
        {
            await NavigateTo(_viewItemForNavigateBack);
        }

        private async Task NavigateTo(ViewItem viewItem)
        {
            if (viewItem.IsAuthorizationRequired && !_userManager.IsAuthorized)
            {
                _lastRichView = _viewManager.LoginViewItem;
                await NavigateToViewItem(_lastRichView, viewItem.ViewKey);
                return;
            }

            if (!string.IsNullOrEmpty(viewItem.RichShellName))
            {
                var richShell = _viewManager.GetRichShell(viewItem.RichShellName);
                if (_lastRichView != richShell)
                {
                    await NavigateToViewItem(richShell);
                    _lastRichView = richShell;
                    _viewItemForNavigateBack = _lastViewItem;
                }
            }

            await NavigateToViewItem(viewItem);
        }

        private async Task NavigateToViewItem(ViewItem viewItem, object navigationParameter = null)
        {
            if(navigationParameter == null)
                await _regionManager.RequestNavigateAsync(viewItem.RegionName, new Uri(viewItem.ViewKey, UriKind.Relative));
            else
                await _regionManager.RequestNavigateAsync(viewItem.RegionName, new Uri(viewItem.ViewKey, UriKind.Relative), new NavigationParameters { { "RequestedView", navigationParameter } });

            foreach (var subViewItem in viewItem.SubViewItems)
            {
                await NavigateTo(subViewItem);
            }
        }

        private readonly IRegionManager _regionManager;
        private readonly IUserManager _userManager;
        private readonly IViewManager _viewManager;
        private ViewItem _lastRichView;
        private ViewItem _lastViewItem;
        private ViewItem _viewItemForNavigateBack;
    }
}