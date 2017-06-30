using System.Threading.Tasks;
using CrossApplication.Core.Contracts;
using CrossApplication.Core.Contracts.Application.Authorization;

namespace CrossApplication.Core.Common
{
    public class UserManager : IUserManager
    {
        public bool IsAuthorized { get; private set; }

        public async Task<bool> LoginAsync(IAuthorizationProvider authorizationProvider)
        {
            var authorized = await authorizationProvider.AuthorizeTask("qq");
            IsAuthorized = authorized != null;
            return IsAuthorized;
        }
    }
}