namespace CrossApplication.Wpf.Application.Shell.RibbonTabs
{
    public partial class AboutView : IBackstageTabViewModel
    {
        public AboutView()
        {
            InitializeComponent();
        }

        public int Position { get; } = 10;
    }
}