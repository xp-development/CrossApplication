using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts
{
    public interface IUserManager
    {
        bool IsAuthorized { get; }
        Task<bool> LoginAsync(string userName);
    }
}