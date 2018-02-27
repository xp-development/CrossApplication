namespace CrossApplication.Core.Contracts.Application.Events
{
    public class ProgressMessageEventPayload
    {
        public int Progress { get; }

        public ProgressMessageEventPayload(int progress)
        {
            Progress = progress;
        }
    }
}