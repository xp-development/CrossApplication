namespace CrossApplication.Mail.Contracts.Messaging
{
    public class MailContact
    {
        public string Initials { get; set; }
        public int UnreadMailCount { get; set; }
        public int TotalMailCount { get; set; }
    }
}