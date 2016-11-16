using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Common.Modules
{
    public class AggregateModuleCatalog : IModuleCatalog
    {
        public ModuleCatalog ModuleCatalog { get; }

        public AggregateModuleCatalog(ModuleCatalog moduleCatalog, IModuleCatalog directoryModuleCatalog)
        {
            ModuleCatalog = moduleCatalog;
            _catalogs.Add(moduleCatalog);
            _catalogs.Add(directoryModuleCatalog);
        }

        public async Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync()
        {
            var moduleInfos = new List<ModuleInfo>();
            foreach (var moduleCatalog in _catalogs)
            {
                foreach (var moduleInfo in await moduleCatalog.GetModuleInfosAsync())
                {
                    moduleInfos.Add(moduleInfo);
                }
            }

            return moduleInfos;
        }

        private readonly List<IModuleCatalog> _catalogs = new List<IModuleCatalog>();
    }
}