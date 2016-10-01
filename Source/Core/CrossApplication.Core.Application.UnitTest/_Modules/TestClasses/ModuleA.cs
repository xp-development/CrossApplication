using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Application.UnitTest._Modules.TestClasses
{
    public class ModuleA : IModule
    {
        public bool IsActivated { get; private set; }

        public Task InitializeAsync()
        {
            return Task.FromResult(true);
        }

        public Task ActivateAsync()
        {
            IsActivated = true;

            return Task.FromResult(true);
        }
    }
}