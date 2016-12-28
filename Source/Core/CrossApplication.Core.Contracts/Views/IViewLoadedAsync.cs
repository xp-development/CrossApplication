using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewLoadedAsync
    {
        Task OnViewLoadedAsync();
    }
}