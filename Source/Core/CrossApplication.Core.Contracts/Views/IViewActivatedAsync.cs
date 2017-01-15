using System.Threading.Tasks;
using Prism.Navigation;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewActivatedAsync
    {
        Task OnViewActivatedAsync(NavigationParameters navigationParameters);
    }
}