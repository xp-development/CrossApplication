using CrossMailing.Wpf.Mail.Shell;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace CrossMailing.Wpf.Mail
{
    public class MailModule : IModule
    {
        public MailModule(IRegionViewRegistry regionViewRegistry)
        {
            _regionViewRegistry = regionViewRegistry;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof (ShellView));
            _regionViewRegistry.RegisterViewWithRegion("RibbonRegion", typeof (RibbonView));
        }

        private readonly IRegionViewRegistry _regionViewRegistry;
    }
}