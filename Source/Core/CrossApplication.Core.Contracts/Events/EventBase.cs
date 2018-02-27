using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossApplication.Core.Contracts.Events
{
    public abstract class EventBase<TPayload> : IEvent<TPayload>
    {
        public void Subscribe(Func<TPayload, Task> func)
        {
            _funcs.Add(func);
        }

        public void Unsubscribe(Func<TPayload, Task> func)
        {
            _funcs.Remove(func);
        }

        public async Task PublishAsync(TPayload payload)
        {
            foreach (var action in _funcs)
            {
                await action(payload);
            }
        }

        private readonly IList<Func<TPayload, Task>> _funcs = new List<Func<TPayload, Task>>();
    }
}