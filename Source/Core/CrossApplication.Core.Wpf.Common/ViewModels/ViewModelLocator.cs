using System;
using System.Windows;
using CrossApplication.Core.Common.Mvvm;

namespace CrossApplication.Core.Wpf.Common.ViewModels
{
    public static class ViewModelLocator
    {
        public static void SetAutoWireViewModel(DependencyObject element, Type value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        public static Type GetAutoWireViewModel(DependencyObject element)
        {
            return (Type) element.GetValue(AutoWireViewModelProperty);
        }

        private static void OnAutoWireViewModelChanged(DependencyObject view, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                return;

            ViewModelProvider.AutoWireViewModelChanged(view, (Type) e.NewValue);
        }

        public static readonly DependencyProperty AutoWireViewModelProperty = DependencyProperty.RegisterAttached("AutoWireViewModel", typeof(Type), typeof(ViewModelLocator), new PropertyMetadata(null, OnAutoWireViewModelChanged));
    }
}