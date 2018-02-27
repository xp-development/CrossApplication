namespace CrossApplication.Core.Contracts.Application.Events
{
    public class StateMessageEventPayload
    {
        public string Message { get; }

        public StateMessageEventPayload(string message)
        {
            Message = message;
        }
    }
}