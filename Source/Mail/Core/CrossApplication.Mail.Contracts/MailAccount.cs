namespace CrossApplication.Mail.Contracts
{
    public class MailAccount
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseTls { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}