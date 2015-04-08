using System.Globalization;
using System.Threading;
using System.Windows;
using CrossMailing.Wpf.Application.Properties;

namespace CrossMailing.Wpf.Application
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetCurrentCulture();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (e.ApplicationExitCode != 0)
                return;

            Settings.Default.Save();
        }

        private static void SetCurrentCulture()
        {
            if (string.IsNullOrWhiteSpace(Settings.Default.ApplicationCulture))
                return;

            var cultureInfo = new CultureInfo("de");
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}