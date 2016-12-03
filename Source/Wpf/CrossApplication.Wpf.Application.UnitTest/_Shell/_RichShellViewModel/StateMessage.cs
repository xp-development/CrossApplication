using System.Collections.Generic;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Net.Contracts.Navigation;
using CrossApplication.Wpf.Application.Shell;
using CrossApplication.Wpf.Application.Shell.RibbonTabs;
using FluentAssertions;
using Moq;
using Prism.Events;
using Prism.Regions;
using Xunit;

namespace CrossApplication.Wpf.Application.UnitTest._Shell._RichShellViewModel
{
    public class StateMessage
    {
        [Fact]
        public async void ShouldSetStateIfStateMessageEventIsPublished()
        {
            var eventAggregator = new EventAggregator();
            var viewModel = new RichShellViewModel(null, new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IBackstageTabViewModel>(), new Mock<IRegionManager>().Object, eventAggregator);
            await viewModel.OnViewLoadedAsync();

            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Test message."));

            viewModel.StateMessage.Should().Be("Test message.");
        }
    }
}