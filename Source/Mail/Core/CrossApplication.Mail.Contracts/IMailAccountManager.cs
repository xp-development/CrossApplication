using System.Collections.Generic;

namespace CrossApplication.Mail.Contracts
{
    public interface IMailAccountManager
    {
        IEnumerable<MailAccount> GetMailAccounts();
    }
}