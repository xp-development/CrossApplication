using System.Collections.Generic;
using System.Threading;
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
            var resetEvent = new AutoResetEvent(false);
            viewModel.PropertyChanged += (sender, args) => resetEvent.Set();

            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Test message."));

            resetEvent.WaitOne(200).Should().BeTrue();
            viewModel.StateMessage.Should().Be("Test message.");
        }

        [Fact]
        public async void ShouldNotRefreshStateIfViewIsUnloaded()
        {
            var eventAggregator = new EventAggregator();
            var viewModel = new RichShellViewModel(null, new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IBackstageTabViewModel>(), new Mock<IRegionManager>().Object, eventAggregator);
            await viewModel.OnViewLoadedAsync();
            var resetEvent = new AutoResetEvent(false);
            viewModel.PropertyChanged += (sender, args) => resetEvent.Set();

            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Test message."));

            resetEvent.WaitOne(1000).Should().BeTrue();
            await viewModel.OnViewUnloadedAsync();
            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Next test message."));

            resetEvent.WaitOne(1200).Should().BeFalse();
            viewModel.StateMessage.Should().Be("Test message.");
        }
    }
}