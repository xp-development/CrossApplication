using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Mail.Contracts.Messaging;

namespace CrossApplication.Mail.Core.Messaging
{
    public class MailAccountManager : IMailAccountManager
    {
        private readonly IStorage _storage;
        private readonly IStringEncryption _stringEncryption;

        public MailAccountManager(IStorage storage, IStringEncryption stringEncryption)
        {
            _storage = storage;
            _stringEncryption = stringEncryption;
        }

        public async Task<IEnumerable<MailAccountSetting>> GetMailAccountSettingsAsync()
        {
            var mailAccountSettings = await _storage.ReadAsync<MailAccountSetting[]>("MailAccountSettings");
            if (mailAccountSettings == null)
            {
                return new List<MailAccountSetting>();
            }

            return mailAccountSettings;
        }
    }
}