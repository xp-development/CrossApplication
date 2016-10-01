using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using Microsoft.Practices.Unity;

namespace CrossApplication.Core.Application.Modules
{
    public class ModuleManager : IModuleManager
    {
        public ModuleManager(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void SetModuleCatalog(IModuleCatalog moduleCatalog)
        {
            _moduleCatalog = moduleCatalog;
        }

        public async Task InizializeAsync(string tag = null)
        {
            foreach (var moduleInfo in _moduleCatalog.GetModuleInfos().Where(x => string.IsNullOrEmpty(tag) || x.Tag == tag))
            {
                var module = _unityContainer.Resolve(moduleInfo.ModuleType) as IModule;
                if (module == null)
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

        private IModuleCatalog _moduleCatalog;
        private readonly IUnityContainer _unityContainer;
        private readonly IDictionary<ModuleInfo, IModule> _modules = new Dictionary<ModuleInfo, IModule>();
    }
}