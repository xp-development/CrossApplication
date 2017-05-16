namespace CrossApplication.Mail.Contracts.Messaging
{
    public class MailAccountSetting
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseTls { get; set; }
        public string UserName { get; set; }
        public string CryptedPassword { get; set; }
    }
}