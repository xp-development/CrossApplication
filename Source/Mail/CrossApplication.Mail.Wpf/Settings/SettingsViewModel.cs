using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Documents;
using CrossApplication.Core.Contracts.Security;
using CrossApplication.Core.Contracts.Settings;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Mail.Contracts.Messaging;
using Prism.Commands;
using Prism.Navigation;

namespace CrossApplication.Mail.Wpf.Settings
{
    public class SettingsViewModel : IViewActivatedAsync, ISettingsChild
    {
        public ObservableCollection<MailAccountSettingViewModel> MailAccountSettings { get; } = new ObservableCollection<MailAccountSettingViewModel>();

        public DelegateCommand NewAccountCommand { get; }

        public SettingsViewModel(IMailAccountManager mailAccountManager, IStringEncryption stringEncryption)
        {
            _mailAccountManager = mailAccountManager;
            _stringEncryption = stringEncryption;

            NewAccountCommand = new DelegateCommand(OnNewAccount);
        }

        private void OnNewAccount()
        {
            MailAccountSettings.Add(new MailAccountSettingViewModel());
        }

        public Task SaveAsync()
        {
            var mailAccountSettings = new List<MailAccountSetting>();
            foreach (var settingsViewModel in MailAccountSettings)
            {
                var mailAccountSetting = settingsViewModel.GetMailAccountSetting();
                if (!string.IsNullOrWhiteSpace(settingsViewModel.Password))
                {
                    mailAccountSetting.CryptedPassword = _stringEncryption.Encrypt(settingsViewModel.Password);
                }

                mailAccountSettings.Add(mailAccountSetting);
            }

            return _mailAccountManager.SaveMailAccountSettingsAsync(mailAccountSettings);
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            if (MailAccountSettings.Count == 0)
            {
                foreach (var mailAccountSetting in await _mailAccountManager.GetMailAccountSettingsAsync())
                    MailAccountSettings.Add(MailAccountSettingViewModel.Create(mailAccountSetting));
            }
        }

        private readonly IMailAccountManager _mailAccountManager;
        private readonly IStringEncryption _stringEncryption;
    }
}