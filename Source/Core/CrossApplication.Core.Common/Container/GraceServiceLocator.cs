using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Events;
using Grace.DependencyInjection;

namespace CrossApplication.Core.Common.Container
{
    public class GraceServiceLocator : IServiceLocator
    {
        public GraceServiceLocator(DependencyInjectionContainer dependencyInjectionContainer)
        {
            throw new System.NotImplementedException();
        }

        public IEvent GetInstance<T>()
        {
            throw new System.NotImplementedException();
        }

        public T GetInstance<T>(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}