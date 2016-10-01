using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IModuleManager
    {
        void SetModuleCatalog(IModuleCatalog moduleCatalog);

        Task InizializeAsync(string tag = "");
        Task ActivateAsync(string tag = "");
    }
}