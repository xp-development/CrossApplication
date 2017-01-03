using System;
using CrossApplication.Core.Common.Mvvm;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins.ViewModels
{
    public static class ViewModelLocator
    {
        public static void SetAutoWireViewModel(BindableObject element, Type value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        public static Type GetAutoWireViewModel(BindableObject element)
        {
            return (Type) element.GetValue(AutoWireViewModelProperty);
        }

        private static void OnAutoWireViewModelChanged(BindableObject view, object oldValue, object newValue)
        {
            if (newValue == null)
                return;

            ViewModelProvider.AutoWireViewModelChanged(view, (Type) newValue);
        }

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", typeof(Type), typeof(ViewModelLocator), null, propertyChanged: OnAutoWireViewModelChanged);
    }
}