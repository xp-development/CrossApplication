using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CrossApplication.Core.Wpf.Common.AttachedProperties
{
    public class ImageExtensions
    {
        public static readonly DependencyProperty UriProperty = DependencyProperty.RegisterAttached("Uri", typeof(string), typeof(ImageExtensions), new PropertyMetadata(default(string), PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var uriSource = new Uri((string) args.NewValue);
            var indexOfDelimiter = uriSource.LocalPath.IndexOf(";", StringComparison.Ordinal);
            var assemblyName = uriSource.LocalPath.Substring(1, indexOfDelimiter - 1);
            var assembly = System.Reflection.Assembly.Load(assemblyName);
            var bitmap = new BitmapImage();

            const string component = ";component";
            var resourceName = assemblyName + "._" + uriSource.LocalPath.Substring(uriSource.LocalPath.IndexOf(component, StringComparison.Ordinal) + component.Length + 1).Replace('/', '.');
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
            }

            ((Image) dependencyObject).Source = bitmap;
        }

        public static void SetUri(DependencyObject element, string value)
        {
            element.SetValue(UriProperty, value);
        }

        public static string GetUri(DependencyObject element)
        {
            return (string) element.GetValue(UriProperty);
        }
    }
}