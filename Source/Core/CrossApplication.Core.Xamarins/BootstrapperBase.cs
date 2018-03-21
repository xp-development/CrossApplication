using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Common.Navigation;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Xamarins.About;
using CrossApplication.Core.Xamarins.Common.Application;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins
{
    public abstract class BootstrapperBase : Core.Common.BootstrapperBase
    {
        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelProvider.SetDataContextCallback((view, viewModel) => { ((BindableObject) view).BindingContext = viewModel; });
        }

        protected override void ConfigureContainer()
        {
            Container.RegisterType<IApplicationProvider, ApplicationProvider>(Lifetime.PerContainer);
            Container.RegisterType<IAboutService, AboutService>();

            base.ConfigureContainer();

            Container.RegisterInstance<IMainNavigationItem>(new MainNavigationItem("About", "About", "Help"));

            Container.RegisterType<object, AboutView>("About", Lifetime.PerContainer);
            var shellViewItem = new ViewItem("About", "");
            Container.Resolve<IViewManager>().AddViewItem(shellViewItem);
        }

        protected override void InitializeShell()
        {

        }
    }
}