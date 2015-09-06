using System;
using System.Threading;
using CrossMailing.Common;
using CrossMailing.Wpf.Application.Shell;
using CrossMailing.Wpf.Application.UnitTest.Properties;
using CrossMailing.Wpf.Common.Events;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Moq;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Xunit;

namespace CrossMailing.Wpf.Application.UnitTest.Shell._RichShellViewModel
{
    public class ManageAccountsCommand
    {
        [Fact]
        public async void ShouldAddNavigationItemIfInitializeModuleEventIsPublished()
        {
            var interactionRequest = new InteractionRequest<INotification>();
            var isRaised = false;
            interactionRequest.Raised += (sender, args) => { isRaised = true; };

            var container = new UnityContainer();
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            container.RegisterInstance(interactionRequest);
            var viewModel = container.Resolve<RichShellViewModel>();

            await viewModel.ManageAccountsCommand.Execute();

            isRaised.Should().BeTrue();
        }
    }
}