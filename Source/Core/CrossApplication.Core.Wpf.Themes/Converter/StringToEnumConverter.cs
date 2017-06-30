using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace CrossApplication.Core.Wpf.Themes.Converter
{
    public class StringToEnumConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!targetType.IsEnum)
                throw new InvalidOperationException($"TargetType '{targetType}' should be an enum.");

            if (value == null)
                return Binding.DoNothing;

            return value.GetType().IsEnum ? value : Enum.Parse(targetType, value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}