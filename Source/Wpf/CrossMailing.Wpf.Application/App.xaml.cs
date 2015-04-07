using System.Windows;
using CrossMailing.Wpf.Application.Properties;

namespace CrossMailing.Wpf.Application
{
    public partial class App
    {
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (e.ApplicationExitCode != 0)
                return;

            Settings.Default.Save();
        }
    }
}