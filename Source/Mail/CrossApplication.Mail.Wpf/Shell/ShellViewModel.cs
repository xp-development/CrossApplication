using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;

namespace CrossApplication.Mail.Wpf.Shell
{
    public class ShellViewModel : IViewActivatedAsync, IViewDeactivatedAsync
    {
        private readonly IMailContactManager _mailContactManager;
        public ObservableCollection<ContactViewModel> Contacts { get; }

        public ShellViewModel(IMailContactManager mailContactManager)
        {
            _mailContactManager = mailContactManager;
            Contacts = new ObservableCollection<ContactViewModel>();
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            foreach (var mailContact in await _mailContactManager.GetMailContactsAsync())
            {
                Contacts.Add(new ContactViewModel { Background = Brushes.DarkKhaki, ShortId = mailContact.Initials });
            }
        }

        public Task OnViewDeactivatedAsync()
        {
            Contacts.Clear();

            return Task.CompletedTask;
        }
    }

    public class ContactViewModel
    {
        public string ShortId { get; set; }
        public Brush Background { get; set; }
    }
}