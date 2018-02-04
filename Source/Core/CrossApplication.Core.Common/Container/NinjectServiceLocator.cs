using System;
using System.Collections.Generic;
using CommonServiceLocator;
using Ninject;

namespace CrossApplication.Core.Common.Container
{
    public class NinjectServiceLocator : ServiceLocatorImplBase
    {
        public NinjectServiceLocator(IKernel kernel)
        {
            _kernel = kernel;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _kernel.Get(serviceType, key);
        }

        private readonly IKernel _kernel;
    }
}