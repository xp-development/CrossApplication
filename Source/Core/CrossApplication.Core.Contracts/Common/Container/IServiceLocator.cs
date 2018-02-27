using CrossApplication.Core.Contracts.Events;

namespace CrossApplication.Core.Contracts.Common.Container
{
    public interface IServiceLocator
    {
        IEvent GetInstance<T>();
        T GetInstance<T>(string name);
    }
}