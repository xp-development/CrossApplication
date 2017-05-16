using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CrossApplication.Mail.Contracts.Messaging
{
    public class MailFolder
    {
        public string Name { get; set; }
        public IEnumerable<MailFolder> Folder { get; } = new ObservableCollection<MailFolder>();
    }
}