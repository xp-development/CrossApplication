namespace CrossApplication.Mail.Xamarins.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            LoadApplication(new Core.Xamarins.App());
        }
    }
}
