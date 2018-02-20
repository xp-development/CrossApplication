using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Mail.Contracts.Messaging;

namespace CrossApplication.Mail.Core.Messaging
{
    public class MailAccountManager : IMailAccountManager
    {
        private readonly IStorage _storage;

        public MailAccountManager(IStorage storage)
        {
            _storage = storage;
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

        public Task SaveMailAccountSettingsAsync(IEnumerable<MailAccountSetting> mailAccountSettings)
        {
            return _storage.SaveAsync("MailAccountSettings", mailAccountSettings.ToArray());
        }
    }
}