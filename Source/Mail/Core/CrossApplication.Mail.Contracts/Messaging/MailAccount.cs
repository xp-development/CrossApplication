using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrossApplication.Mail.Contracts.Messaging
{
    public class MailAccount
    {
        public string Name { get; set; }
        public IList<MailFolder> Folders { get; } = new ObservableCollection<MailFolder>();
    }
}