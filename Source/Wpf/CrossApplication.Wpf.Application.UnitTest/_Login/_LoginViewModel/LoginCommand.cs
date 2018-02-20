using System.Globalization;
using System.Threading;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Authorization;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Wpf.Application.Login;
using FluentAssertions;
using Moq;
using Xunit;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Wpf.Application.UnitTest._Login._LoginViewModel
{
    public class LoginCommand
    {
        private Mock<IUserManager> _userManagerMock;
        private Mock<INavigationService> _navigationServiceMock;

        [Fact]
        public void SuccessfulLoginShouldNavigateToLastRequestedView()
        {
            var viewModel = CreateViewModel(true);

//            viewModel.LoginCommand.Execute(null);

            viewModel.Message.Should().BeNullOrEmpty();
            _navigationServiceMock.Verify(x => x.NavigateToAsync("MyRequestedView"), Times.Once);
        }

        [Fact]
        public void FailedLoginShouldSetMessage()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            var viewModel = CreateViewModel(false);

//            viewModel.LoginCommand.Execute(null);

            viewModel.Message.Should().Be("Login failed.");
            _navigationServiceMock.Verify(x => x.NavigateToAsync("MyRequestedView"), Times.Never);
        }

        private LoginViewModel CreateViewModel(bool loginResult)
        {
            _userManagerMock = new Mock<IUserManager>();
            _userManagerMock.Setup(x => x.LoginAsync(It.IsAny<IAuthorizationProvider>())).ReturnsAsync(loginResult);
            _navigationServiceMock = new Mock<INavigationService>();
            var viewModel = new LoginViewModel(_userManagerMock.Object, _navigationServiceMock.Object, null);
            viewModel.OnViewActivatingAsync(new NavigationParameters {{"RequestedView", "MyRequestedView"}});
            return viewModel;
        }
    }
}