using System.Collections.Generic;

namespace CrossApplication.Core.Contracts.Application.Modules
{
    public interface IModuleCatalog
    {
        IEnumerable<ModuleInfo> GetModuleInfos();
    }
}