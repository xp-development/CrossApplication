using CrossApplication.Core.Common.ViewModels;
using Xamarin.Forms;

namespace CrossApplication.Core.Xamarins.ViewModels
{
    public static class ViewModelLocator
    {
        public static void SetAutoWireViewModel(BindableObject element, bool value)
        {
            element.SetValue(AutoWireViewModelProperty, value);
        }

        public static bool GetAutoWireViewModel(BindableObject element)
        {
            return (bool)element.GetValue(AutoWireViewModelProperty);
        }

        private static void OnAutoWireViewModelChanged(BindableObject view, object oldValue, object newValue)
        {
            if (!(bool)newValue)
                return;

            ViewModelProvider.AutoWireViewModelChanged(view);
        }

        public static readonly BindableProperty AutoWireViewModelProperty = BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), false, propertyChanged: OnAutoWireViewModelChanged);
    }
}