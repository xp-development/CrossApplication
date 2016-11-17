using System;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using Moq;
using Prism.Regions;
using Xunit;

namespace CrossApplication.Wpf.Common.UnitTest.Navigation._NavigationService
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
        public void ShouldNavigateToLoginPageIfAuthorizationIsRequired()
        {
            var loginViewItem = new ViewItem("NavigationLoginKey", RegionNames.RichRegion, false);
            _viewManager.AddViewItem(loginViewItem);
            _viewManager.AddViewItem(new ViewItem("NavigationKey", RegionNames.MainRegion, true));
            _viewManager.LoginViewItem = loginViewItem;

            _navigationService.NavigateTo("NavigationKey");

            _regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.RichRegion, new Uri("NavigationLoginKey", UriKind.Relative), new NavigationParameters { { "RequestedView", "NavigationKey" } }));
        }

        [Fact]
        public void ShouldNavigateToUri()
        {
            _viewManager.AddViewItem(new ViewItem("NavigationKey", RegionNames.MainRegion, false));

            _navigationService.NavigateTo("NavigationKey");

            _regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.MainRegion, new Uri("NavigationKey", UriKind.Relative)));
        }
    }
}