using System;
using CrossMailing.Wpf.Common;
using CrossMailing.Wpf.Common.Events;
using CrossMailing.Wpf.Mail.Shell;
using FluentAssertions;
using Microsoft.Practices.Unity;
using Moq;
using Xunit;

namespace CrossMailing.Wpf.Mail.UnitTest._MailModule
{
    public class Initialize : TesterBase
    {
        [Fact]
        public void ShouldRegisterShellViewToUnityContainer()
        {
            var module = TestContainer.Resolve<MailModule>();

            module.Initialize();

            UnityContainerMock.Verify(item => item.RegisterType(It.Is<Type>(param => param == typeof(object)), It.Is<Type>(param => param == typeof(ShellView)), It.Is<string>(param => param == typeof(ShellView).FullName), It.IsAny<LifetimeManager>()));
        }

        [Fact]
        public void ShouldNavigateToShellViewIfActivateModuleEventWasFiredWithAnotherId()
        {
            var module = TestContainer.Resolve<MailModule>();
            module.Initialize();

            EventAggregatorMock.Object.GetEvent<ActivateModuleEvent>().Publish(new ActivateModulePayload(Guid.NewGuid()));

            RegionManagerMock.Verify(item => item.RequestNavigate(It.IsAny<string>(), It.IsAny<Uri>()), Times.Never);
        }
    }
}