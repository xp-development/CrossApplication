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
        private readonly Mock<IRegionManager> _regionManagerMock;
        private readonly IViewManager _viewManager;
        private readonly INavigationService _navigationService;

        public NavigateTo()
        {
            _regionManagerMock = new Mock<IRegionManager>();
            _viewManager = new ViewManager();
            _navigationService = new NavigationService(_regionManagerMock.Object, _viewManager, new Mock<IUserManager>().Object);
        }

        [Fact]
        public void ShouldNavigateToUri()
        {
            _viewManager.AddViewItem(new ViewItem("NavigationKey", false, RegionNames.MainRegion));

            _navigationService.NavigateTo("NavigationKey");
            
            _regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.MainRegion, new Uri("NavigationKey", UriKind.Relative)));
        }

        [Fact]
        public void ShouldNavigateToLoginPageIfAuthorizationIsRequired()
        {
            _viewManager.AddViewItem(new ViewItem("NavigationLoginKey", false, RegionNames.RichRegion));
            _viewManager.AddViewItem(new ViewItem("NavigationKey", true, RegionNames.MainRegion));

            _navigationService.NavigateTo("NavigationLoginKey");

            _regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.RichRegion, new Uri("NavigationLoginKey", UriKind.Relative)));
        }
    }
}