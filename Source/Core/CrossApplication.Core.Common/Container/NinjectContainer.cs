using System;
using System.Collections.Generic;
using CrossApplication.Core.Contracts.Common.Container;
using Ninject;

namespace CrossApplication.Core.Common.Container
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

        public void RegisterType<TInterface1, TInterface2, TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface1, TInterface2
        {
            var binding = _kernel.Bind<TInterface1, TInterface2>().To<TImplementation>();
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

        public void RegisterInstance<TInterface>(TInterface instance, Lifetime lifetime = Lifetime.PerResolve)
        {
            var binding = _kernel.Bind<TInterface>().ToConstant(instance);
            if (lifetime == Lifetime.PerContainer)
            {
                binding.InSingletonScope();
            }
        }

        public void RegisterType<T>(string name, Lifetime lifetime = Lifetime.PerResolve)
        {
            var binding = _kernel.Bind<T>().To<T>();
            if (lifetime == Lifetime.PerContainer)
            {
                binding.InSingletonScope();
            }

            binding.Named(name);
        }

        public void RegisterType<TInterface, TImplementation>(string name, Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface
        {
            var binding = _kernel.Bind<TInterface>().To<TImplementation>();
            if (lifetime == Lifetime.PerContainer)
            {
                binding.InSingletonScope();
            }

            binding.Named(name);
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

        private readonly IKernel _kernel;
    }
}