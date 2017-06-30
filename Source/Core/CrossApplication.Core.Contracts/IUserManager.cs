using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Authorization;

namespace CrossApplication.Core.Contracts
{
    public interface IUserManager
    {
        bool IsAuthorized { get; }
        Task<bool> LoginAsync(IAuthorizationProvider authorizationProvider);
    }
}