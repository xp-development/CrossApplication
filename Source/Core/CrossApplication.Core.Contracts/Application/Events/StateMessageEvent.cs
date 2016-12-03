using Prism.Events;

namespace CrossApplication.Core.Contracts.Application.Events
{
    public class StateMessageEvent : PubSubEvent
    {
        public string Message { get; }

        public StateMessageEvent(string message)
        {
            Message = message;
        }
    }
}