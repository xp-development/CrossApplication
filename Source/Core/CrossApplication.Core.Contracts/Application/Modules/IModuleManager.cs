using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IModuleManager
    {
        Task InizializeAsync(string tag = "");
        Task ActivateAsync(string tag = "");
    }
}