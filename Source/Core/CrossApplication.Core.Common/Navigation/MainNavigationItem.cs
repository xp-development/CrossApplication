using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Core.Common.Navigation
{
    public class MainNavigationItem : IMainNavigationItem
    {
        public MainNavigationItem(string label, string navigationKey, string glyph)
        {
            Label = label;
            NavigationKey = navigationKey;
            Glyph = glyph;
        }

        public string Label { get; }
        public string Glyph { get; }
        public string NavigationKey { get; }
    }
}