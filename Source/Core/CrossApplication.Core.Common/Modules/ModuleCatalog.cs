using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Common.Modules
{
    public class ModuleCatalog : IModuleCatalog
    {
        public Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync()
        {
            return Task.FromResult<IEnumerable<ModuleInfo>>(_moduleInfos);
        }

        public void AddModuleInfo(ModuleInfo moduleInfo)
        {
            if (moduleInfo.ModuleType == null)
            {
                throw new ArgumentException("ModuleType is not set.");
            }

            if (_moduleInfos.Any(x => x.ModuleType == moduleInfo.ModuleType))
            {
                throw new ArgumentException($"ModuleType {moduleInfo.ModuleType} was already added.");
            }

            _moduleInfos.Add(moduleInfo);
        }

        private readonly IList<ModuleInfo> _moduleInfos = new List<ModuleInfo>();
    }
}
