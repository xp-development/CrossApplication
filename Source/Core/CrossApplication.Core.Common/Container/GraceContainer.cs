using System;
using System.Collections.Generic;
using CrossApplication.Core.Contracts.Common.Container;
using Grace.DependencyInjection;

namespace CrossApplication.Core.Common.Container
{
    public class GraceContainer : IContainer
    {
        public GraceContainer(DependencyInjectionContainer kernel)
        {
            _kernel = kernel;
        }

        public void RegisterType<TInterface, TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface
        {
            _kernel.Configure(x =>
            {
                var configuration = x.Export<TInterface>().As<TImplementation>();
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public void RegisterType<TInterface1, TInterface2, TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface1, TInterface2
        {
            _kernel.Configure(x =>
            {
                var configuration = x.Export<TInterface1>().As<TImplementation>();
                x.Export<TInterface2>().As<TImplementation>();
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public void RegisterType<TImplementation>(Lifetime lifetime = Lifetime.PerResolve)
        {
            _kernel.Configure(x =>
            {
                var configuration = x.Export<TImplementation>().As<TImplementation>();
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public void RegisterInstance<TInterface>(TInterface instance, Lifetime lifetime = Lifetime.PerResolve)
        {
            _kernel.Configure(x =>
            {
                var configuration = x.ExportInstance(instance);
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public void RegisterType<T>(string name, Lifetime lifetime = Lifetime.PerResolve)
        {
            _kernel.Configure(x =>
            {
                var configuration = x.Export<T>().AsKeyed<T>(name);
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public void RegisterType<TInterface, TImplementation>(string name, Lifetime lifetime = Lifetime.PerResolve)
            where TImplementation : TInterface
        {
            _kernel.Configure(x =>
            {
                var configuration = x.Export<TInterface>().AsKeyed<TImplementation>(name);
                if (lifetime == Lifetime.PerContainer)
                    configuration.Lifestyle.Singleton();
            });
        }

        public TInterface Resolve<TInterface>()
        {
            return _kernel.Locate<TInterface>();
        }

        public object Resolve(Type type)
        {
            return _kernel.Locate(type);
        }

        public IEnumerable<TInterface> ResolveAll<TInterface>()
        {
            return _kernel.LocateAll<TInterface>();
        }

        private readonly DependencyInjectionContainer _kernel;
    }
}