namespace CrossApplication.Core.Contracts.Navigation
{
    public interface IMainNavigationItem
    {
        string Label { get; }
        string Glyph { get; }
        string NavigationKey { get; }
    }
}