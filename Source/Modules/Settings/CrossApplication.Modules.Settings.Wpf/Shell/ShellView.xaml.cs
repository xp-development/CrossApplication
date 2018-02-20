using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using CrossApplication.Core.Common.Mvvm;

namespace CrossApplication.Modules.Settings.Wpf.Shell
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }
    }

    public class SuppressNavigationItemSelectionConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is NavigationItem)
                return Binding.DoNothing;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new SuppressNavigationItemSelectionConverter();
        }
    }
}
