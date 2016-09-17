namespace CrossApplication.Wpf.Contracts
{
    public interface INavigationService : Core.Contracts.INavigationService
    {
        void RegisterView<TView>(string navigationKey, string regionName);
    }
}