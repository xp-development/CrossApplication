using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Xamarins;
using CrossApplication.Core.Xamarins.Shell;
using Xamarin.Forms;

namespace CrossApplication.Xamarin
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void CreateShell()
        {
            _shell = Container.Resolve<ApplicationShellView>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainPage = _shell;

            var viewManager = Container.Resolve<IViewManager>();
            viewManager.RegisterRichShell(RegionNames.RichRegion, new ViewItem(typeof(RichShellView).FullName, RegionNames.RichRegion));
//            viewManager.LoginViewItem = new ViewItem(typeof(LoginView).FullName, RegionNames.RichRegion);
        }

        private ApplicationShellView _shell;
    }
}