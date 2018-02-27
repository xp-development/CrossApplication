using System;
using System.Collections.Generic;
using Grace.DependencyInjection;
using Microsoft.Practices.ServiceLocation;

namespace CrossApplication.Core.Common.Container
{
    public class GraceServiceLocator : ServiceLocatorImplBase
    {
        public GraceServiceLocator(DependencyInjectionContainer kernel)
        {
            _kernel = kernel;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return _kernel.LocateAll(serviceType);
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return _kernel.Locate(serviceType, key);
        }

        private readonly DependencyInjectionContainer _kernel;
    }
}