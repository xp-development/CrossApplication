namespace CrossApplication.Core.Contracts.Navigation
{
    public interface ISettingsNavigationItem
    {
        string Label { get; }
        string Glyph { get; }
        string NavigationKey { get; }
    }
}