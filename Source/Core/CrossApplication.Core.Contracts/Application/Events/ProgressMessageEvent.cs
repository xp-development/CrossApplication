using Prism.Events;

namespace CrossApplication.Core.Contracts.Application.Events
{
    public class ProgressMessageEvent : PubSubEvent
    {
        public int Progress { get; }

        public ProgressMessageEvent()
        {}

        public ProgressMessageEvent(int progress)
        {
            Progress = progress;
        }
    }
}