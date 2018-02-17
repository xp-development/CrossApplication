namespace CrossApplication.Core.Contracts.Navigation
{
    public interface IInfrastructureNavigationItem
    {
        string Label { get; }
        string Glyph { get; }
        string NavigationKey { get; }
    }
}