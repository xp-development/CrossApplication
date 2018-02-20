using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Mail.Contracts.Messaging;

namespace CrossApplication.Mail.Wpf.Settings
{
    public class MailAccountSettingViewModel : ViewModelBase
    {
        public string Server
        {
            get => _server;
            set
            {
                _server = value;
                NotifyPropertyChanged();
            }
        }

        public int Port
        {
            get => _port;
            set
            {
                _port = value;
                NotifyPropertyChanged();
            }
        }

        public bool UseTls
        {
            get => _useTls;
            set
            {
                _useTls = value;
                NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyPropertyChanged();
            }
        }

        public MailAccountSetting GetMailAccountSetting()
        {
            var mailAccountSetting = _mailAccountSetting ?? new MailAccountSetting();
            mailAccountSetting.Server = Server;
            mailAccountSetting.Port = Port;
            mailAccountSetting.UseTls = UseTls;
            mailAccountSetting.UserName = UserName;

            return mailAccountSetting;
        }

        private MailAccountSettingViewModel(MailAccountSetting mailAccountSetting)
        {
            _mailAccountSetting = mailAccountSetting;
            Server = mailAccountSetting.Server;
            Port = mailAccountSetting.Port;
            UseTls = mailAccountSetting.UseTls;
            UserName = mailAccountSetting.UserName;
        }

        public MailAccountSettingViewModel()
        {
        }

        public static MailAccountSettingViewModel Create(MailAccountSetting mailAccountSetting)
        {
            return new MailAccountSettingViewModel(mailAccountSetting);
        }

        private readonly MailAccountSetting _mailAccountSetting;
        private string _password;
        private int _port;
        private string _server;
        private string _userName;
        private bool _useTls;
    }
}