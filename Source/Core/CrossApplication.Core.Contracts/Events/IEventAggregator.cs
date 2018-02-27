namespace CrossApplication.Core.Contracts.Events
{
    public interface IEventAggregator
    {
        IEvent<TEventPayload> GetEvent<TEventPayload>();
    }
}