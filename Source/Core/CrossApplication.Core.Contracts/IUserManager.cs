namespace CrossApplication.Core.Contracts
{
    public interface IUserManager
    {
        bool IsAuthorized { get; }
    }
}