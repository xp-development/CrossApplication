using System.Linq;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Mail.Core.Messaging;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrossApplication.Mail.Core.UnitTest._Messaging._MailAccountManager
{
    public class GetMailAccountSettingsAsync
    {
        [Fact]
        public async void Usage()
        {
            var storageMock = new Mock<IStorage>();
            storageMock.Setup(x => x.ReadAsync<MailAccountSetting[]>("MailAccountSettings")).ReturnsAsync(new[]
            {
                new MailAccountSetting
                {
                    Server = "imap.test.server",
                    Port = 993,
                    UserName = "testUserName",
                    CryptedPassword = "xyz",
                    UseTls = true
                },
                new MailAccountSetting
                {
                    Server = "imap.test2.server",
                    Port = 659,
                    UserName = "testUserName2",
                    CryptedPassword = "xyz2"
                }
            });
            var manager = new MailAccountManager(storageMock.Object);

            var mailAccountSettings = (await manager.GetMailAccountSettingsAsync()).ToArray();

            mailAccountSettings.Length.Should().Be(2);
            mailAccountSettings[0].Server.Should().Be("imap.test.server");
            mailAccountSettings[0].Port.Should().Be(993);
            mailAccountSettings[0].UserName.Should().Be("testUserName");
            mailAccountSettings[0].CryptedPassword.Should().Be("xyz");
            mailAccountSettings[0].UseTls.Should().BeTrue();
            mailAccountSettings[1].Server.Should().Be("imap.test2.server");
            mailAccountSettings[1].Port.Should().Be(659);
            mailAccountSettings[1].UserName.Should().Be("testUserName2");
            mailAccountSettings[1].CryptedPassword.Should().Be("xyz2");
            mailAccountSettings[1].UseTls.Should().BeFalse();
        }

        [Fact]
        public async void ShouldReturnEmptyListIfNoSettingExists()
        {
            var storageMock = new Mock<IStorage>();
            var manager = new MailAccountManager(storageMock.Object);

            var mailAccountSettings = (await manager.GetMailAccountSettingsAsync()).ToArray();

            mailAccountSettings.Length.Should().Be(0);
        }
    }
}