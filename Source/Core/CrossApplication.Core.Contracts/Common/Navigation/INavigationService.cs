using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Common.Navigation
{
    public interface INavigationService
    {
        Task NavigateToAsync(string navigationKey);
        Task NavigateBackAsync();
    }
}