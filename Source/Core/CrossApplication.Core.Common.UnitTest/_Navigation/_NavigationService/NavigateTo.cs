using System;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using Moq;
using Prism.Navigation;
using Xunit;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Core.Common.UnitTest._Navigation._NavigationService
{
    public class NavigateTo
    {
        public NavigateTo()
        {
            _regionManagerMock = new Mock<IRegionManager>();
            _viewManager = new ViewManager();
            _navigationService = new NavigationService(_regionManagerMock.Object, _viewManager, new Mock<IUserManager>().Object);
        }

        private readonly Mock<IRegionManager> _regionManagerMock;
        private readonly IViewManager _viewManager;
        private readonly INavigationService _navigationService;

        [Fact]
        public async void ShouldNavigateToLoginPageIfAuthorizationIsRequired()
        {
            var loginViewItem = new ViewItem("NavigationLoginKey", "RichRegion");
            _viewManager.AddViewItem(loginViewItem);
            _viewManager.AddViewItem(new ViewItem("NavigationKey", "MainRegion", true));
            _viewManager.LoginViewItem = loginViewItem;

            await _navigationService.NavigateToAsync("NavigationKey");

            _regionManagerMock.Verify(x => x.RequestNavigateAsync("RichRegion", new Uri("NavigationLoginKey", UriKind.Relative), new NavigationParameters { { "RequestedView", "NavigationKey" } }));
        }

        [Fact]
        public async void ShouldNavigateToUri()
        {
            _viewManager.AddViewItem(new ViewItem("NavigationKey", "MainRegion"));

            await _navigationService.NavigateToAsync("NavigationKey");

            _regionManagerMock.Verify(x => x.RequestNavigateAsync("MainRegion", new Uri("NavigationKey", UriKind.Relative)));
        }
    }
}