using System;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Common.UnitTest._Modules.TestModules
{
    [Module]
    public class ModuleB : IModule
    {
        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task ActivateAsync()
        {
            throw new NotImplementedException();
        }
    }
}