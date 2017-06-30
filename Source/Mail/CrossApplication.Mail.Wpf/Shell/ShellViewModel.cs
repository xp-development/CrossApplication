using System.Collections.ObjectModel;
using System.Windows.Media;

namespace CrossApplication.Mail.Wpf.Shell
{
    public class ShellViewModel
    {
        public ObservableCollection<ContactViewModel> Contacts { get; private set; }

        public ShellViewModel()
        {
            Contacts = new ObservableCollection<ContactViewModel>();
            
            Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = "JS"});
            Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = "AB"});
            Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = "CD"});
            Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = "EF"});
            Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = "GH"});
            
            
        }
    }

    public class ContactViewModel
    {
        public string ShortId { get; set; }
        public Brush Background { get; set; }
    }
}