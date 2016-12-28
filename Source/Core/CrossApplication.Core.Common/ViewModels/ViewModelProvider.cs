using System;

namespace CrossApplication.Core.Common.ViewModels
{
    public static class ViewModelProvider
    {
        public static void AutoWireViewModelChanged(object view)
        {
            var viewModel = GetViewModelByView(view);
            _dataContextCallback(view, viewModel);
            _viewEventCallback(view, viewModel);
        }

        public static void SetViewModelFactoryMethod(Func<Type, object> factoryMethod)
        {
            _factoryMethod = factoryMethod;
        }

        private static object GetViewModelByView(object view)
        {
            var viewModelType = _getTypeCallback(view.GetType());
            return _factoryMethod(viewModelType);
        }

        public static void SetDataContextCallback(Action<object, object> dataContextCallback)
        {
            _dataContextCallback = dataContextCallback;
        }

        public static void SetGetTypeCallback(Func<Type, Type> getTypeCallback)
        {
            _getTypeCallback = getTypeCallback;
        }

        public static void SetViewEventCallback(Action<object, object> viewEventCallback)
        {
            _viewEventCallback = viewEventCallback;
        }

        private static Func<Type, object> _factoryMethod;
        private static Action<object, object> _dataContextCallback;
        private static Func<Type, Type> _getTypeCallback;
        private static Action<object, object> _viewEventCallback;
    }
}