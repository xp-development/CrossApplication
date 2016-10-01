using System;
using System.Collections.Generic;
using System.Linq;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Application.Modules
{
    public class ModuleCatalog : IModuleCatalog
    {
        public IEnumerable<ModuleInfo> GetModuleInfos()
        {
            return _moduleInfo;
        }

        public void AddModuleInfo(ModuleInfo moduleInfo)
        {
            if (moduleInfo.ModuleType == null)
            {
                throw new ArgumentException("ModuleType is not set.");
            }

            if (_moduleInfo.Any(x => x.ModuleType == moduleInfo.ModuleType))
            {
                throw new ArgumentException($"ModuleType {moduleInfo.ModuleType} was already added.");
            }
            _moduleInfo.Add(moduleInfo);
        }

        private readonly IList<ModuleInfo> _moduleInfo = new List<ModuleInfo>();
    }
}
