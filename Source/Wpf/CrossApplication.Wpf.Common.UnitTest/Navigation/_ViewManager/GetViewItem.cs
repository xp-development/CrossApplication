using System;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Wpf.Common.UnitTest.Navigation._ViewManager
{
    public class GetViewItem
    {
        [Fact]
        public void Usage()
        {
            const string viewKey = "ViewKey";
            const string regionName = "RegionName";
            var viewType = typeof(TestView);
            var viewManager = new ViewManager();
            viewManager.AddViewItem(new ViewItem(viewKey, viewType, false, regionName));

            var viewItem = viewManager.GetViewItem(viewKey);

            viewItem.ViewKey.Should().Be(viewKey);
            viewItem.RegionName.Should().Be(regionName);
            viewItem.ViewType.Should().Be(viewType);
            viewItem.IsAuthorizationRequired.Should().BeFalse();
            viewItem.SubViewItems.Count.Should().Be(0);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionIfViewKeyWasNotAdded()
        {
            var viewManager = new ViewManager();

            var action = new Action(() =>viewManager.GetViewItem("ViewKey"));

            action.ShouldThrow<ArgumentException>().Which.ParamName.Should().Be("viewKey");
        }
    }
}