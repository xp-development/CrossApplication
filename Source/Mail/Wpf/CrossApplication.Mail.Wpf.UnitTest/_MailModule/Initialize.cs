using System;
using CrossApplication.Mail.Wpf.Shell;
using Microsoft.Practices.Unity;
using Moq;
using Xunit;

namespace CrossApplication.Mail.Wpf.UnitTest._MailModule
{
    public class Initialize : TesterBase
    {
        [Fact]
        public async void ShouldRegisterShellViewToUnityContainer()
        {
            var module = TestContainer.Resolve<Module>();

            await module.InitializeAsync();

            UnityContainerMock.Verify(item => item.RegisterType(It.Is<Type>(param => param == typeof(object)), It.Is<Type>(param => param == typeof(ShellView)), It.Is<string>(param => param == typeof(ShellView).FullName), It.IsAny<LifetimeManager>()));
        }

        [Fact]
        public async void ShouldNavigateToShellViewIfModuleWasActivated()
        {
            var module = TestContainer.Resolve<Module>();
            await module.InitializeAsync();
            await module.ActivateAsync();

            RegionManagerMock.Verify(item => item.RequestNavigate(It.IsAny<string>(), It.IsAny<Uri>()), Times.Never);
        }
    }
}