using System;
using System.Globalization;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins.ValueConverters
{
  public class VersionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (!(value is Version))
      {
        return value;
      }

      return ((Version) value).ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}