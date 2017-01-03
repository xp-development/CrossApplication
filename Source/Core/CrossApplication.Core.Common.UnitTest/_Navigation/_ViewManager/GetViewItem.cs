using System;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Navigation._ViewManager
{
    public class GetViewItem
    {
        [Fact]
        public void ShouldThrowArgumentExceptionIfViewKeyWasNotAdded()
        {
            var viewManager = new ViewManager();

            var action = new Action(() => viewManager.GetViewItem("ViewKey"));

            action.ShouldThrow<ArgumentException>().Which.ParamName.Should().Be("viewKey");
        }

        [Fact]
        public void Usage()
        {
            const string viewKey = "ViewKey";
            const string regionName = "RegionName";
            var viewManager = new ViewManager();
            viewManager.AddViewItem(new ViewItem(viewKey, regionName));

            var viewItem = viewManager.GetViewItem(viewKey);

            viewItem.ViewKey.Should().Be(viewKey);
            viewItem.RegionName.Should().Be(regionName);
            viewItem.IsAuthorizationRequired.Should().BeFalse();
            viewItem.SubViewItems.Count.Should().Be(0);
        }
    }
}