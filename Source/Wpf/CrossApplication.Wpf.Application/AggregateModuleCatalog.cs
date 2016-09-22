using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Modularity;

namespace CrossApplication.Wpf.Application
{
    public class AggregateModuleCatalog : IModuleCatalog
    {
        public ReadOnlyCollection<IModuleCatalog> Catalogs => _catalogs.AsReadOnly();

        public IEnumerable<ModuleInfo> Modules
        {
            get { return Catalogs.SelectMany(x => x.Modules); }
        }

        public AggregateModuleCatalog()
        {
            _catalogs.Add(new ModuleCatalog());
        }

        public void AddCatalog(IModuleCatalog catalog)
        {
            if (catalog == null)
            {
                throw new ArgumentNullException(nameof(catalog));
            }

            _catalogs.Add(catalog);
        }

        public IEnumerable<ModuleInfo> GetDependentModules(ModuleInfo moduleInfo)
        {
            var catalog = _catalogs.Single(x => x.Modules.Contains(moduleInfo));
            return catalog.GetDependentModules(moduleInfo);
        }

        public IEnumerable<ModuleInfo> CompleteListWithDependencies(IEnumerable<ModuleInfo> modules)
        {
            var modulesGroupedByCatalog = modules.GroupBy(module => _catalogs.Single(catalog => catalog.Modules.Contains(module)));
            return modulesGroupedByCatalog.SelectMany(x => x.Key.CompleteListWithDependencies(x));
        }

        public void Initialize()
        {
            foreach (var catalog in Catalogs)
            {
                catalog.Initialize();
            }
        }

        public void AddModule(ModuleInfo moduleInfo)
        {
            _catalogs[0].AddModule(moduleInfo);
        }

        private readonly List<IModuleCatalog> _catalogs = new List<IModuleCatalog>();
    }
}