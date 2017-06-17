using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;

namespace CrossApplication.Core.Wpf.Common
{
    [Module(Tag = ModuleTags.Infrastructure)]
    public class Module : IModule
    {
        public Task InitializeAsync()
        {
            return Task.FromResult(false);
        }

        public Task ActivateAsync()
        {
            return Task.FromResult(false);
        }
    }
}