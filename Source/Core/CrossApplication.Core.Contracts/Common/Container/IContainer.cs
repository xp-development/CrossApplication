using System;
using System.Collections.Generic;

namespace CrossApplication.Core.Contracts.Common.Container
{
    public interface IContainer
    {
        void RegisterType<TInterface, TImplementation>(Lifetime lifetime = Lifetime.PerResolve) where TImplementation : TInterface;
        void RegisterType<TImplementation>(Lifetime lifetime = Lifetime.PerResolve);
        void RegisterInstance<TInterface>(TInterface instance, Lifetime lifetime = Lifetime.PerResolve);
        void RegisterType<T>(string name, Lifetime lifetime = Lifetime.PerResolve);
        void RegisterType<TInterface, TImplementation>(string name, Lifetime lifetime = Lifetime.PerResolve) where TImplementation : TInterface;
        TInterface Resolve<TInterface>();
        object Resolve(Type type);
        IEnumerable<TInterface> ResolveAll<TInterface>();
    }
}