using System.Threading.Tasks;
using Prism.Navigation;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewActivatingAsync
    {
        Task OnViewActivatingAsync(NavigationParameters navigationParameters);
    }
}