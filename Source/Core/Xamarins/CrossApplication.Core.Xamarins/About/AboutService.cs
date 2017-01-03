using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;

namespace CrossApplication.Core.Xamarins.About
{
    public class AboutService : IAboutService
    {
        public AboutService(IModuleCatalog moduleCatalog)
        {
            _moduleCatalog = moduleCatalog;
        }

        public Task<Version> GetVersionAsync()
        {
            return Task.FromResult(new Version(2016, 0));
        }

        public Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync()
        {
            return _moduleCatalog.GetModuleInfosAsync();
        }

        private readonly IModuleCatalog _moduleCatalog;
    }
}