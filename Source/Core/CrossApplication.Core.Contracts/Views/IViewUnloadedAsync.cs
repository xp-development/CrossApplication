using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Views
{
    public interface IViewUnloadedAsync
    {
        Task OnViewUnloadedAsync();
    }
}