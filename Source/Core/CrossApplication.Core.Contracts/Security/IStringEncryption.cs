namespace CrossApplication.Core.Contracts.Security
{
    public interface IStringEncryption
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}