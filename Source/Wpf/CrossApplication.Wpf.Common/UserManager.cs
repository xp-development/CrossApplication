using System.Threading.Tasks;
using CrossApplication.Core.Contracts;

namespace CrossApplication.Wpf.Common
{
    public class UserManager : IUserManager
    {
        public bool IsAuthorized { get; private set; }

        public Task<bool> LoginAsync(string userName)
        {
            if(userName == "42")
                IsAuthorized = true;

            return Task.FromResult(IsAuthorized);
        }
    }
}