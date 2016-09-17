using System;
using System.Globalization;
using System.Threading;
using CrossApplication.Core.Common;
using CrossApplication.Wpf.Common.Navigation;
using CrossApplication.Wpf.Common.UnitTest.Properties;
using FluentAssertions;
using Xunit;

namespace CrossApplication.Wpf.Common.UnitTest.Navigation._NavigationItem
{
    public class Label
    {
        [Theory]
        [InlineData("en", "english text")]
        [InlineData("de", "deutscher text")]
        public void ShouldGetLocalizedLabel(string culture, string expectedLabelText)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            var item = new NavigationItem(null, Guid.Empty, new ResourceValue(typeof(Resources), "TestResourceString"));

            item.Label.Should().Be(expectedLabelText);
        }
    }
}