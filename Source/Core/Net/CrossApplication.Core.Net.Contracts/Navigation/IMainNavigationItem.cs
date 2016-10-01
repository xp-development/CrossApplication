namespace CrossApplication.Core.Net.Contracts.Navigation
{
    public interface IMainNavigationItem
    {
        string Label { get; }
        string NavigationKey { get; }
    }
}