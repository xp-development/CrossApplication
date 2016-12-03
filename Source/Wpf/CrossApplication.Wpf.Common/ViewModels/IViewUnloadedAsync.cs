using System.Threading.Tasks;

namespace CrossApplication.Wpf.Common.ViewModels
{
    public interface IViewUnloadedAsync
    {
        Task OnViewUnloadedAsync();
    }
}