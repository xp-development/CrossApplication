using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Contracts.Application.Services
{
    public interface IAboutService
    {
        Task<Version> GetVersionAsync();
        Task<IEnumerable<ModuleInfo>> GetModuleInfosAsync();
    }
}