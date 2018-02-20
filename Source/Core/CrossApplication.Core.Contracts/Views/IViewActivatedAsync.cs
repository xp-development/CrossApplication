using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Common.Navigation;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewActivatedAsync
    {
        Task OnViewActivatedAsync(NavigationParameters navigationParameters);
    }
}