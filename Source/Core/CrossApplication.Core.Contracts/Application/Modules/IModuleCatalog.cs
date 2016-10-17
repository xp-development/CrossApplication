using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IModuleCatalog
    {
        Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync();
    }
}