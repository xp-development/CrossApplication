using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Events;
using Grace.DependencyInjection;

namespace CrossApplication.Core.Common.Container
{
    public class GraceServiceLocator : IServiceLocator
    {
        private readonly DependencyInjectionContainer _dependencyInjectionContainer;

        public GraceServiceLocator(DependencyInjectionContainer dependencyInjectionContainer)
        {
            _dependencyInjectionContainer = dependencyInjectionContainer;
        }

        public T GetInstance<T>()
        {
            return _dependencyInjectionContainer.Locate<T>();
        }

        public T GetInstance<T>(string name)
        {
            return _dependencyInjectionContainer.Locate<T>(withKey: name);
        }
    }
}