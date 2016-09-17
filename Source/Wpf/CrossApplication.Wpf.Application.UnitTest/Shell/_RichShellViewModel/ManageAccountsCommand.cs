using CrossApplication.Wpf.Application.Shell;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Xunit;

namespace CrossApplication.Wpf.Application.UnitTest.Shell._RichShellViewModel
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