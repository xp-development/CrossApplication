using System;
using System.Threading.Tasks;

namespace CrossApplication.Xamarin
{
	public partial class App
	{
		public App ()
		{
			InitializeComponent();

		    AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

	    private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
	    {
        }

	    public async Task StartAsync()
	    {
            await new Bootstrapper().Run();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
