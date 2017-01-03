using System;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Core.Common.UnitTest._Navigation._ViewManager
{
    public class AddViewItem
    {
        [Fact]
        public void ShouldThrowArgumentExceptionIfViewKeyIfAlreadyRegistered()
        {
            var viewManager = new ViewManager();
            viewManager.AddViewItem(new ViewItem("ViewKey", "RegionName", false));

            var action = new Action(() => viewManager.AddViewItem(new ViewItem("ViewKey", "RegionName", false)));
            action.ShouldThrow<ArgumentException>().Which.ParamName.Should().Be("viewItem");
        }
    }
}