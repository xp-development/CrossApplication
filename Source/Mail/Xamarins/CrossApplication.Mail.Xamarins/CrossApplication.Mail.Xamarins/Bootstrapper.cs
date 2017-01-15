using CrossApplication.Core.Common.Modules;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Xamarins;

namespace CrossApplication.Mail.Xamarins
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var aggregateModuleCatalog = (ModuleCatalog) base.CreateModuleCatalog();
            aggregateModuleCatalog.AddModuleInfo(new ModuleInfo {ModuleType = typeof(Module), Tag = ModuleTags.Infrastructure});
            return aggregateModuleCatalog;
        }
    }
}