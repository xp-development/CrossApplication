using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Xamarins;
using CrossApplication.Xamarin.Login;
using CrossApplication.Xamarin.Shell;

namespace CrossApplication.Xamarin
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void CreateShell()
        {
        }

        protected override void InitializeShell()
        {
            var viewManager = Container.Resolve<IViewManager>();
            viewManager.RegisterRichShell(RegionNames.RichRegion, new ViewItem(typeof(RichShellView).FullName, RegionNames.RichRegion));
            viewManager.LoginViewItem = new ViewItem(typeof(LoginView).FullName, RegionNames.RichRegion);
        }

        protected override void ConfigureContainer()
        {
            RegisterViews();
            base.ConfigureContainer();
        }

        private void RegisterViews()
        {
            Container.RegisterType<RichShellViewModel>(Lifetime.PerContainer);
            Container.RegisterType<object, RichShellView>(typeof(RichShellView).FullName, Lifetime.PerContainer);

            Container.RegisterType<LoginViewModel>();
            Container.RegisterType<object, LoginView>(typeof(LoginView).FullName);
        }
    }
}