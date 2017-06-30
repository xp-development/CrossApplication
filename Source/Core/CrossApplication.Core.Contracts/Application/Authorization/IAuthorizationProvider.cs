using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Application.Authorization
{
    public interface IAuthorizationProvider
    {
        string Name { get; }
        string Glyph { get; }

        /// <summary>
        /// Returns null if authorization fails otherwise credentials.
        /// </summary>
        Task<object> AuthorizeTask(string userToken);
    }
}