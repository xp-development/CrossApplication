using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;
using Xamarin.Forms;

namespace CrossApplication.Mail.Xamarin.Shell
{
    public class ShellViewModel : IViewActivatedAsync, IViewDeactivatedAsync
    {
        public ObservableCollection<ContactViewModel> Contacts { get; }

        public ShellViewModel(IMailContactManager mailContactManager)
        {
            _mailContactManager = mailContactManager;
            Contacts = new ObservableCollection<ContactViewModel>();
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            foreach (var mailContact in await _mailContactManager.GetMailContactsAsync()) Contacts.Add(new ContactViewModel {Background = Color.DarkKhaki, ShortId = mailContact.Initials});
        }

        public Task OnViewDeactivatedAsync()
        {
            Contacts.Clear();

            return Task.CompletedTask;
        }

        private readonly IMailContactManager _mailContactManager;
    }

    public class ContactViewModel
    {
        public string ShortId { get; set; }
        public Color Background { get; set; }
    }
}