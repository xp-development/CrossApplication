using System;
using System.Globalization;
using System.Threading;
using CrossMailing.Common;
using CrossMailing.Wpf.Common.Navigation;
using FluentAssertions;
using Xunit;

namespace CrossMailing.Wpf.Common.UnitTest.Navigation._NavigationItem
{
    public class Label
    {
        [Theory]
        [InlineData("en", "english text")]
        [InlineData("de", "deutscher text")]
        public void ShouldGetLocalizedLabel(string culture, string expectedLabelText)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            var item = new NavigationItem(null, Guid.Empty, new ResourceValue(typeof(Properties.Resources), "TestResourceString"));

            item.Label.Should().Be(expectedLabelText);
        }
    }
}