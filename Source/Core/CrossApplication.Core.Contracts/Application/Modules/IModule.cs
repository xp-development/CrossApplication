using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IModule
    {
        Task InitializeAsync();
        Task ActivateAsync();
    }
}