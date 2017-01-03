using System;

namespace CrossApplication.Core.Common.Mvvm
{
    public static class ViewModelProvider
    {
        public static void AutoWireViewModelChanged(object view, Type viewModelType)
        {
            var viewModel = _factoryMethod(viewModelType);
            _dataContextCallback(view, viewModel);
            _viewEventCallback(view, viewModel);
        }

        public static void SetViewModelFactoryMethod(Func<Type, object> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        public static void SetDataContextCallback(Action<object, object> dataContextCallback)
        {
            _dataContextCallback = dataContextCallback;
        }

        public static void SetViewEventCallback(Action<object, object> viewEventCallback)
        {
            _viewEventCallback = viewEventCallback;
        }

        private static Action<object, object> _dataContextCallback;
        private static Action<object, object> _viewEventCallback;
        private static Func<Type, object> _factoryMethod;
    }
}