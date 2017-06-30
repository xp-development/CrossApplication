using CrossApplication.Core.Wpf.Contracts.Backstages;

namespace CrossApplication.Wpf.Application.Backstages
{
    public class BackstageNavigationItem : IBackstageNavigationItem
    {
        public BackstageNavigationItem(string label, string navigationKey, string glyph)
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