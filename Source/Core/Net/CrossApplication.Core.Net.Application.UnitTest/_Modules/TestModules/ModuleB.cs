using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Net.Application.UnitTest._Modules.TestModules
{
    [Module]
    public class ModuleB : IModule
    {
        public Task InitializeAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task ActivateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}