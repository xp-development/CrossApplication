using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Mail.Contracts.Messaging;
using CrossApplication.Mail.Core.Properties;
using MailMessaging.Plain.Contracts;
using MailMessaging.Plain.Contracts.Commands;
using MailMessaging.Plain.Contracts.Services;
using Prism.Events;

namespace CrossApplication.Mail.Core.Messaging
{
    public class MailManager : IMailManager
    {
        public MailManager(IMailAccountManager accountManager, IMailMessenger messenger, ITagService tagService, IStringEncryption stringEncryption, IEventAggregator eventAggregator)
        {
            _accountManager = accountManager;
            _messenger = messenger;
            _tagService = tagService;
            _stringEncryption = stringEncryption;
            _stateMessageEvent = eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>();
        }

        public async Task<IEnumerable<MailAccount>> GetAccountsAsync()
        {
            var mailAccountSettings = await _accountManager.GetMailAccountSettingsAsync();
            var mailAccounts = new List<MailAccount>();
            foreach (var mailAccountSetting in mailAccountSettings)
            {
                var mailAccount = new MailAccount();
                var connectResult = await _messenger.ConnectAsync(new Account(mailAccountSetting.Server, mailAccountSetting.Port, mailAccountSetting.UseTls));
                if (connectResult != ConnectResult.Connected)
                    continue;
                _stateMessageEvent.Publish(new StateMessageEvent(string.Format(Resources.ConnectedWithSERVER, mailAccountSetting.Server)));

                var loginResponse = await _messenger.SendAsync(new LoginCommand(_tagService, mailAccountSetting.UserName, _stringEncryption.Decrypt(mailAccountSetting.CryptedPassword)));
                if (loginResponse.Result != ResponseResult.OK)
                    continue;

                _stateMessageEvent.Publish(new StateMessageEvent(string.Format(Resources.USERNAMELoggedIn, mailAccountSetting.UserName)));

                mailAccount.Name = mailAccountSetting.UserName;
                var listResponse = await _messenger.SendAsync(new ListCommand(_tagService, "", "*"));
                foreach (var folder in listResponse.Folders)
                {
                    mailAccount.Folders.Add(new MailFolder {Name = folder.Name});
                }
                _stateMessageEvent.Publish(new StateMessageEvent(Resources.FoldersSynchronized));

                var logoutResponse = await _messenger.SendAsync(new LogoutCommand(_tagService));
                if (logoutResponse.Result != ResponseResult.OK)
                    continue;

                _stateMessageEvent.Publish(new StateMessageEvent(string.Format(Resources.USERNAMELoggedOut, mailAccountSetting.UserName)));

                _messenger.Disconnect();

                mailAccounts.Add(mailAccount);
            }

            return mailAccounts;
        }

        private class Account : IAccount
        {
            public Account(string server, int port, bool useTls)
            {
                Server = server;
                Port = port;
                UseTls = useTls;
            }

            public string Server { get; }
            public int Port { get; }
            public bool UseTls { get; }
        }

        private readonly IMailAccountManager _accountManager;
        private readonly IMailMessenger _messenger;
        private readonly PubSubEvent<StateMessageEvent> _stateMessageEvent;
        private readonly IStringEncryption _stringEncryption;
        private readonly ITagService _tagService;
    }
}