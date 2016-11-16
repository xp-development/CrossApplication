using System.Windows;

namespace CrossApplication.Wpf.Common.ViewModels
{
    public static class ViewModelLocator
    {
        public static void SetAutoWireViewModel(DependencyObject element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        public static bool GetAutoWireViewModel(DependencyObject element)
        {
            return (bool) element.GetValue(AutoWireViewModelProperty);
        }

        private static void OnAutoWireViewModelChanged(DependencyObject view, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool) e.NewValue)
                return;

            ViewModelProvider.AutoWireViewModelChanged(view);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), new PropertyMetadata(false, OnAutoWireViewModelChanged));
    }
}