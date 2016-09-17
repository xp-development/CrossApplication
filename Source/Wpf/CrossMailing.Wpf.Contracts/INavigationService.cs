namespace CrossMailing.Wpf.Contracts
{
    public interface INavigationService : CrossMailing.Contracts.INavigationService
    {
        void RegisterView<TView>(string navigationKey, string regionName);
    }
}