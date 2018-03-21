using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Net.Common.Services;

namespace CrossApplication.Core.Net.Common
{
    public abstract class BootstrapperBase : Core.Common.BootstrapperBase
    {
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IAboutService, AboutService>();
        }
    }
}