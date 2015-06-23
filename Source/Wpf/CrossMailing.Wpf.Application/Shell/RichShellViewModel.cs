using System.Collections.ObjectModel;

namespace CrossMailing.Wpf.Application.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<string> NavigationItems { get; set; }

        public RichShellViewModel()
        {
            NavigationItems = new ObservableCollection<string>
            {
                "E-Mail",
                "Calendar",
                "Contacts"
            };
        }
    }
}