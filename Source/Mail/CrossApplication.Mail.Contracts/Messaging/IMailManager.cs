using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApplication.Mail.Contracts.Messaging
{
    public interface IMailManager
    {
        Task<IEnumerable<MailAccount>> GetAccountsAsync();
    }
}