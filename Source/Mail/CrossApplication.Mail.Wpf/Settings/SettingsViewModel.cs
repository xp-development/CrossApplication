using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Core.Contracts.Settings;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;

namespace CrossApplication.Mail.Wpf.Settings
{
    public class SettingsViewModel : ViewModelBase, IViewActivatedAsync, ISettingsChild
    {
        public ObservableCollection<MailAccountSettingViewModel> MailAccountSettings { get; } = new ObservableCollection<MailAccountSettingViewModel>();

        public DelegateCommand<object, object> NewAccountCommand { get; }
        public DelegateCommand<object, object> DeleteSelectedAccountCommand { get; }

        public MailAccountSettingViewModel SelectedMailAccountSetting
        {
            get => _selectedMailAccountSetting;
            set
            {
                _selectedMailAccountSetting = value;
                NotifyPropertyChanged();
            }
        }

        public SettingsViewModel(IMailAccountManager mailAccountManager, IStringEncryption stringEncryption)
        {
            _mailAccountManager = mailAccountManager;
            _stringEncryption = stringEncryption;

            NewAccountCommand = new DelegateCommand<object, object>(OnNewAccount);
            DeleteSelectedAccountCommand = new DelegateCommand<object, object>(OnDeleteSelectedAccount, args => SelectedMailAccountSetting != null);
        }

        public Task SaveAsync()
        {
            var mailAccountSettings = new List<MailAccountSetting>();
            foreach (var settingsViewModel in MailAccountSettings)
            {
                var mailAccountSetting = settingsViewModel.GetMailAccountSetting();
                if (!string.IsNullOrWhiteSpace(settingsViewModel.Password)) mailAccountSetting.CryptedPassword = _stringEncryption.Encrypt(settingsViewModel.Password);

                mailAccountSettings.Add(mailAccountSetting);
            }

            return _mailAccountManager.SaveMailAccountSettingsAsync(mailAccountSettings);
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            if (MailAccountSettings.Count == 0)
                foreach (var mailAccountSetting in await _mailAccountManager.GetMailAccountSettingsAsync())
                    MailAccountSettings.Add(MailAccountSettingViewModel.Create(mailAccountSetting));
        }

        private Task OnDeleteSelectedAccount(object args)
        {
            MailAccountSettings.Remove(SelectedMailAccountSetting);
            SelectedMailAccountSetting = null;

            return Task.CompletedTask;
        }

        private Task OnNewAccount(object args)
        {
            MailAccountSettings.Add(new MailAccountSettingViewModel());

            return Task.CompletedTask;
        }

        private readonly IMailAccountManager _mailAccountManager;
        private readonly IStringEncryption _stringEncryption;
        private MailAccountSettingViewModel _selectedMailAccountSetting;
    }
}