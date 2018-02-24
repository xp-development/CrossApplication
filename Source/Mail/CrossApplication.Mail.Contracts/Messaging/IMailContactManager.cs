using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApplication.Mail.Contracts.Messaging
{
    public interface IMailContactManager
    {
        Task<IEnumerable<MailContact>> GetMailContactsAsync();
    }
}