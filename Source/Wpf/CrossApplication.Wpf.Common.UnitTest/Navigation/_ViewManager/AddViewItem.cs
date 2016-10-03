using System;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Contracts.Navigation;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Wpf.Common.UnitTest.Navigation._ViewManager
{
    public class AddViewItem
    {
        [Fact]
        public void ShouldThrowArgumentExceptionIfViewKeyIfAlreadyRegistered()
        {
            var viewManager = new ViewManager();
            viewManager.AddViewItem(new ViewItem("ViewKey", false, "RegionName"));

            var action = new Action(() => viewManager.AddViewItem(new ViewItem("ViewKey", false, "RegionName")));
            action.ShouldThrow<ArgumentException>().Which.ParamName.Should().Be("viewItem");
        }
    }
}