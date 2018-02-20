using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApplication.Mail.Contracts.Messaging
{
    public interface IMailAccountManager
    {
        Task<IEnumerable<MailAccountSetting>> GetMailAccountSettingsAsync();
        Task SaveMailAccountSettingsAsync(IEnumerable<MailAccountSetting> mailAccountSettings);
    }
}