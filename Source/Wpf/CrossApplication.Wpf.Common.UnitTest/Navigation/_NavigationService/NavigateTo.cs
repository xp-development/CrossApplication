using System;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using Moq;
using Prism.Regions;
using Xunit;

namespace CrossApplication.Wpf.Common.UnitTest.Navigation._NavigationService
{
    public class NavigateTo
    {
        [Fact]
        public void ShouldNavigateToUri()
        {
            var regionManagerMock = new Mock<IRegionManager>();
            var viewManager = new ViewManager();
            viewManager.AddViewItem(new ViewItem("NavigationKey", typeof(TestView), false, RegionNames.MainRegion));

            var navigationService = new NavigationService(regionManagerMock.Object, viewManager);

            navigationService.NavigateTo("NavigationKey");
            
            regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(TestView).FullName, UriKind.Relative)));
        }
    }
}