using System.Collections.Generic;
using System.Threading;
using CrossApplication.Core.Common.Events;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Wpf.Application.Shell;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrossApplication.Wpf.Application.UnitTest._Shell._RichShellViewModel
{
    public class StateMessage
    {
        [Fact]
        public async void ShouldSetStateIfStateMessageEventIsPublished()
        {
            var eventAggregator = new EventAggregator(null);
            var viewModel = new RichShellViewModel(new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IInfrastructureNavigationItem>(), eventAggregator);
            await viewModel.OnViewActivatingAsync(null);
            var resetEvent = new AutoResetEvent(false);
            viewModel.PropertyChanged += (sender, args) => resetEvent.Set();

            await eventAggregator.GetEvent<StateMessageEventPayload>().PublishAsync(new StateMessageEventPayload("Test message."));

            resetEvent.WaitOne(200).Should().BeTrue();
            viewModel.StateMessage.Should().Be("Test message.");
        }

        [Fact]
        public async void ShouldNotRefreshStateIfViewIsUnloaded()
        {
            var eventAggregator = new EventAggregator(null);
            var viewModel = new RichShellViewModel(new List<IMainNavigationItem>(), new Mock<INavigationService>().Object, new List<IInfrastructureNavigationItem>(), eventAggregator);
            await viewModel.OnViewActivatingAsync(null);
            var resetEvent = new AutoResetEvent(false);
            viewModel.PropertyChanged += (sender, args) => resetEvent.Set();

            await eventAggregator.GetEvent<StateMessageEventPayload>().PublishAsync(new StateMessageEventPayload("Test message."));

            resetEvent.WaitOne(1000).Should().BeTrue();
            await viewModel.OnViewDeactivatedAsync();
            await eventAggregator.GetEvent<StateMessageEventPayload>().PublishAsync(new StateMessageEventPayload("Next test message."));

            resetEvent.WaitOne(1200).Should().BeFalse();
            viewModel.StateMessage.Should().Be("Test message.");
        }
    }
}