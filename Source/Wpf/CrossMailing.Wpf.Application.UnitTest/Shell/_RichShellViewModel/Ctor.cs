using System;
using CrossMailing.Common;
using CrossMailing.Wpf.Application.Shell;
using CrossMailing.Wpf.Application.UnitTest.Properties;
using CrossMailing.Wpf.Common.Events;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xunit;

namespace CrossMailing.Wpf.Application.UnitTest.Shell._RichShellViewModel
{
    public class Ctor
    {
        [Fact]
        public void ShouldAddNavigationItemIfInitializeModuleEventIsPublished()
        {
            var container = new UnityContainer();
            container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            var viewModel = container.Resolve<RichShellViewModel>();

            var eventAggregator = container.Resolve<IEventAggregator>();

            eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(Guid.Parse("380D8ED5-6824-4824-8329-92574EDC3674"), new ResourceValue(typeof(Resources), "TestString")));
            eventAggregator.GetEvent<InitializeModuleEvent>().Publish(new InitializeModulePayload(Guid.Parse("5F240138-68E1-4BDF-B522-99A3319ED22E"), new ResourceValue(typeof(Resources), "TestString")));

            viewModel.NavigationItems.Count.Should().Be(2);
        }
    }
}