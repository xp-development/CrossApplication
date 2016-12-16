using System.Collections.Generic;
using System.Linq;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Mail.Core.Messaging;
using FluentAssertions;
using MailMessaging.Plain.Contracts;
using MailMessaging.Plain.Contracts.Commands;
using MailMessaging.Plain.Contracts.Services;
using Moq;
using Prism.Events;
using Xunit;

namespace CrossApplication.Mail.Core.UnitTest._Messaging._MailManager
{
    public class GetAccountsAsync
    {
        [Fact]
        public async void Usage()
        {
            var mailMessengerMock = new Mock<IMailMessenger>();
            mailMessengerMock.Setup(x => x.ConnectAsync(It.IsAny<IAccount>())).ReturnsAsync(ConnectResult.Connected);
            mailMessengerMock.Setup(x => x.SendAsync(It.IsAny<LoginCommand>())).ReturnsAsync(LoginCommand.LoginResponse.Create("A001", "A001 OK Logged in."));
            mailMessengerMock.Setup(x => x.SendAsync(It.IsAny<ListCommand>())).ReturnsAsync(ListCommand.ListResponse.Create("A002", "* LIST (\\qq) \"/\" folder1\r\n* LIST (\\qq) \"/\" folder2\r\nA002 OK listed\r\n"));
            mailMessengerMock.Setup(x => x.SendAsync(It.IsAny<LogoutCommand>())).ReturnsAsync(LogoutCommand.LogoutResponse.Create("A003", "A003 OK Logged out."));
            var mailAccountManagerMock = new Mock<IMailAccountManager>();
            mailAccountManagerMock.Setup(x => x.GetMailAccountSettingsAsync()).ReturnsAsync(new List<MailAccountSetting> { new MailAccountSetting { UserName = "myUserName" } });
            var eventAggregator = new Mock<IEventAggregator>();
            eventAggregator.Setup(x => x.GetEvent<PubSubEvent<StateMessageEvent>>()).Returns(new PubSubEvent<StateMessageEvent>());
            var manager = new MailManager(mailAccountManagerMock.Object, mailMessengerMock.Object, new Mock<ITagService>().Object, new Mock<IStringEncryption>().Object, eventAggregator.Object);

            var mailAccounts = (await manager.GetAccountsAsync()).ToArray();

            mailAccounts.Length.Should().Be(1);
            mailAccounts[0].Name.Should().Be("myUserName");
            mailAccounts[0].Folders.Count().Should().Be(2);
            mailAccounts[0].Folders.ElementAt(0).Name.Should().Be("folder1");
            mailAccounts[0].Folders.ElementAt(1).Name.Should().Be("folder2");
        }
    }
}