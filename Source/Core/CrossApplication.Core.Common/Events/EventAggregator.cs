using System;
using System.Collections.Generic;
using CrossApplication.Core.Contracts.Common.Container;
using CrossApplication.Core.Contracts.Events;

namespace CrossApplication.Core.Common.Events
{
    public class EventAggregator : IEventAggregator
    {
        public EventAggregator(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IEvent<TEventPayload> GetEvent<TEventPayload>()
        {
            if (_events.TryGetValue(typeof(IEvent<TEventPayload>), out var @event))
                return (IEvent<TEventPayload>) @event;

            @event = _serviceLocator.GetInstance<Event<TEventPayload>>();
            _events.Add(typeof(IEvent<TEventPayload>), @event);
            return (IEvent<TEventPayload>) @event;
        }

        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<Type, IEvent> _events = new Dictionary<Type, IEvent>();
    }
}