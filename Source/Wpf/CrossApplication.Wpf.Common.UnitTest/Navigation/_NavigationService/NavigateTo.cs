using System;
using CrossApplication.Wpf.Common.Navigation;
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
            var navigationService = new NavigationService(regionManagerMock.Object);
            navigationService.RegisterView<TestView>("NavigationKey", RegionNames.MainRegion);

            navigationService.NavigateTo("NavigationKey");
            
            regionManagerMock.Verify(x => x.RequestNavigate(RegionNames.MainRegion, new Uri(typeof(TestView).FullName, UriKind.Relative)));
        }
    }
}