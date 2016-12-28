using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewLoadingAsync
    {
        Task OnViewLoadingAsync();
    }
}