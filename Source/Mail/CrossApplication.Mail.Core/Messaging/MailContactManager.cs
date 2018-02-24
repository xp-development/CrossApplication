using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Mail.Contracts.Messaging;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Security;

namespace CrossApplication.Mail.Core.Messaging
{
    public class MailContactManager : IMailContactManager
    {
        private readonly IMailAccountManager _accountManager;
        private readonly IStringEncryption _stringEncryption;

        public MailContactManager(IMailAccountManager accountManager, IStringEncryption stringEncryption)
        {
            _accountManager = accountManager;
            _stringEncryption = stringEncryption;
        }

        public async Task<IEnumerable<MailContact>> GetMailContactsAsync()
        {
            var accountSettings = await _accountManager.GetMailAccountSettingsAsync();
            foreach (var accountSetting in accountSettings)
            {
                var imapClient = new ImapClient();
                await imapClient.ConnectAsync(accountSetting.Server, accountSetting.Port, accountSetting.UseTls ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

                await imapClient.AuthenticateAsync(accountSetting.UserName, _stringEncryption.Decrypt(accountSetting.CryptedPassword));

                var list = await imapClient.GetFoldersAsync(new FolderNamespace('/', "*"));

                return list.Select(x => new MailContact {Initials = x.Name});
            }

            return new List<MailContact>();
        }
    }
}