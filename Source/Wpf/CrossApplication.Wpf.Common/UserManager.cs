using CrossApplication.Core.Contracts;

namespace CrossApplication.Wpf.Common
{
    public class UserManager : IUserManager
    {
        public bool IsAuthorized => false;
    }
}