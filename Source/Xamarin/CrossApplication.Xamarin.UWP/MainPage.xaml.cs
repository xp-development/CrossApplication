namespace CrossApplication.Xamarin.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            Start();
        }

        private async void Start()
        {
            var app = new Xamarin.App();
            await app.StartAsync();
            LoadApplication(app);
        }
    }
}
