using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Core.Contracts.Settings;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;
using Prism.Commands;

namespace CrossApplication.Mail.Wpf.Settings
{
    public class SettingsViewModel : ViewModelBase, IViewActivatedAsync, ISettingsChild
    {
        public ObservableCollection<MailAccountSettingViewModel> MailAccountSettings { get; } = new ObservableCollection<MailAccountSettingViewModel>();

        public DelegateCommand NewAccountCommand { get; }
        public DelegateCommand DeleteSelectedAccountCommand { get; }

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

            NewAccountCommand = new DelegateCommand(OnNewAccount);
            DeleteSelectedAccountCommand = new DelegateCommand(OnDeleteSelectedAccount, () => SelectedMailAccountSetting != null);
            DeleteSelectedAccountCommand.ObservesProperty(() => SelectedMailAccountSetting);
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

        private void OnDeleteSelectedAccount()
        {
            MailAccountSettings.Remove(SelectedMailAccountSetting);
            SelectedMailAccountSetting = null;
        }

        private void OnNewAccount()
        {
            MailAccountSettings.Add(new MailAccountSettingViewModel());
        }

        private readonly IMailAccountManager _mailAccountManager;
        private readonly IStringEncryption _stringEncryption;
        private MailAccountSettingViewModel _selectedMailAccountSetting;
    }
}