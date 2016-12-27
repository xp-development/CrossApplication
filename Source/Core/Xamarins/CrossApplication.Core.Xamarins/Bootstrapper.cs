using CrossApplication.Core.Common;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins
{
    public class Bootstrapper : BootstrapperBase
    {
        protected override void CreateShell()
        {
            _shell = ServiceLocator.Current.GetInstance<MainPage>();
        }

        protected override void InitializeShell()
        {
           Application.Current.MainPage = _shell;
        }

        private MainPage _shell;
    }
}