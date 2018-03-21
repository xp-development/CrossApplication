using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Common.Container;

namespace CrossApplication.Core.Common.Modules
{
    public class ModuleManager : IModuleManager
    {
        public ModuleManager(IContainer container, IModuleCatalog moduleCatalog)
        {
            _container = container;
            _moduleCatalog = moduleCatalog;
        }

        public async Task InizializeAsync(string tag = null)
        {
            var moduleInfos = await _moduleCatalog.GetModuleInfosAsync();
            foreach (var moduleInfo in moduleInfos.Where(x => string.IsNullOrEmpty(tag) || x.Tag == tag))
            {
                if (!(_container.Resolve(moduleInfo.ModuleType) is IModule module))
                {
                    throw new ArgumentException($"{moduleInfo.ModuleType} does not inherit from IModule.");
                }

                await module.InitializeAsync();
                _modules.Add(moduleInfo, module);
            }
        }

        public async Task ActivateAsync(string tag = null)
        {
            foreach (var module in _modules.Where(x => string.IsNullOrEmpty(tag) || x.Key.Tag == tag))
            {
                await module.Value.ActivateAsync();
            }
        }

        private readonly IContainer _container;

        private readonly IModuleCatalog _moduleCatalog;
        private readonly IDictionary<ModuleInfo, IModule> _modules = new Dictionary<ModuleInfo, IModule>();
    }
}