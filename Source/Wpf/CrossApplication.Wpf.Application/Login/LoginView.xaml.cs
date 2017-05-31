using System.Windows;

namespace CrossApplication.Wpf.Application.Login
{
    public partial class LoginView
    {
        public LoginView()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Loaded -= OnLoaded;

            UserName.Focus();
        }
    }
}