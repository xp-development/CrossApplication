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

            ConfigureViewModelLocator();

            new Bootstrapper().Run();

            SetCurrentCulture();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            if (e.ApplicationExitCode != 0)
                return;

            Settings.Default.Save();
        }

        private static void ConfigureViewModelLocator()
        {
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