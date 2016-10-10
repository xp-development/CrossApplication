using System.Collections.Generic;
using System.Linq;
using CrossApplication.Core.Application.Modules;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Application.Modules
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

        public IEnumerable<ModuleInfo> GetModuleInfos()
        {
            return _catalogs.SelectMany(moduleCatalog => moduleCatalog.GetModuleInfos());
        }

        private readonly List<IModuleCatalog> _catalogs = new List<IModuleCatalog>();
    }
}