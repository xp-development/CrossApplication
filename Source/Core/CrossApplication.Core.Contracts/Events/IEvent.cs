using System;
using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Events
{
    public interface IEvent<TPayload> : IEvent
    {
        void Subscribe(Func<TPayload, Task> func);
        void Unsubscribe(Func<TPayload, Task> func);
        Task PublishAsync(TPayload payload);
    }

    public interface IEvent
    {
    }
}