using System.Threading.Tasks;

namespace CrossApplication.Wpf.Common.ViewModels
{
    public interface IViewLoadedAsync
    {
        Task OnViewLoadedAsync();
    }
}