using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;

namespace CrossApplication.Core.Net.Common.Services
{
    public class AboutService : IAboutService
    {
        public AboutService(IModuleCatalog moduleCatalog)
        {
            _moduleCatalog = moduleCatalog;
        }

        public Task<Version> GetVersionAsync()
        {
            return Task.Run(() => Assembly.GetExecutingAssembly().GetName().Version);
        }

        public Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync()
        {
            return _moduleCatalog.GetModuleInfosAsync();
        }

        private readonly IModuleCatalog _moduleCatalog;
    }
}