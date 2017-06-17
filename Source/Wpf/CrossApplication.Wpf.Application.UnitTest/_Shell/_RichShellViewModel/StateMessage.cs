using System.Collections.Generic;
using System.Threading;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Wpf.Contracts.Backstages;
using CrossApplication.Wpf.Application.Shell;
using FluentAssertions;
using Moq;
using Prism.Events;
using Xunit;

namespace CrossApplication.Wpf.Application.UnitTest._Shell._RichShellViewModel
{
    public class StateMessage
    {
        [Fact]
        public async void ShouldSetStateIfStateMessageEventIsPublished()
        {
            var eventAggregator = new EventAggregator();
            var viewModel = new RichShellViewModel(null, new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IBackstageNavigationItem>(), eventAggregator);
            await viewModel.OnViewActivatingAsync(null);
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
            var viewModel = new RichShellViewModel(null, new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IBackstageNavigationItem>(), eventAggregator);
            await viewModel.OnViewActivatingAsync(null);
            var resetEvent = new AutoResetEvent(false);
            viewModel.PropertyChanged += (sender, args) => resetEvent.Set();

            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Test message."));

            resetEvent.WaitOne(1000).Should().BeTrue();
            await viewModel.OnViewDeactivatedAsync();
            eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>().Publish(new StateMessageEvent("Next test message."));

            resetEvent.WaitOne(1200).Should().BeFalse();
            viewModel.StateMessage.Should().Be("Test message.");
        }
    }
}