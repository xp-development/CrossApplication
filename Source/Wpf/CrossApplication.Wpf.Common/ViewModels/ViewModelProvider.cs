using System;
using System.Globalization;
using System.Reflection;
using System.Windows;

namespace CrossApplication.Wpf.Common.ViewModels
{
    public static class ViewModelProvider
    {
        public static void AutoWireViewModelChanged(DependencyObject view)
        {
            var viewModel = GetViewModelByView(view);
            var frameworkElement = (FrameworkElement) view;
            frameworkElement.DataContext = viewModel;
            var viewLoaded = viewModel as IViewLoadedAsync;
            if (viewLoaded != null)
                frameworkElement.Loaded += FrameworkElementOnLoaded;
        }

        private static async void FrameworkElementOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var frameworkElement = (FrameworkElement) sender;
            frameworkElement.Loaded -= FrameworkElementOnLoaded;
            await ((IViewLoadedAsync) frameworkElement.DataContext).OnViewLoadedAsync();
        }

        public static void SetViewModelFactoryMethod(Func<Type, object> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        private static object GetViewModelByView(DependencyObject view)
        {
            var viewType = view.GetType();
            var viewModelType = Type.GetType(string.Format(CultureInfo.InvariantCulture, "{0}Model, {1}", viewType.FullName, viewType.GetTypeInfo().Assembly.FullName), name => viewType.GetTypeInfo().Assembly, null, true);
            return _factoryMethod(viewModelType);
        }

        private static Func<Type, object> _factoryMethod;
    }
}