using System;
using System.Collections.Generic;
using CrossApplication.Core.Contracts.Common.Container;
using Ninject;

namespace CrossApplication.Core.Net.Common.Container
{
    public class NinjectContainer : IContainer
    {
        public NinjectContainer(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void RegisterType<TInterface, TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface
        {
            var binding = _kernel.Bind<TInterface>().To<TImplementation>();
            if (lifetime == Lifetime.PerContainer)
            {
                binding.InSingletonScope();
            }
        }

        public void RegisterType<TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
        {
            var binding = _kernel.Bind<TImplementation>().To<TImplementation>();
            if (lifetime == Lifetime.PerContainer)
            {
                binding.InSingletonScope();
            }
        }

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _kernel.Bind<TInterface>().ToConstant(instance);
        }

        public TInterface Resolve<TInterface>()
        {
            return _kernel.Get<TInterface>();
        }

        public object Resolve(Type type)
        {
            return _kernel.Get(type);
        }

        public IEnumerable<TInterface> ResolveAll<TInterface>()
        {
            return _kernel.GetAll<TInterface>();
        }

        public void RegisterType<T>(string name)
        {
            _kernel.Bind<T>().To<T>().Named(name);
        }

        public void RegisterType<TInterface, TImplementation>(string name)
            where TImplementation : TInterface
        {
            _kernel.Bind<TInterface>().To<TImplementation>().Named(name);
        }

        private readonly IKernel _kernel;
    }
}