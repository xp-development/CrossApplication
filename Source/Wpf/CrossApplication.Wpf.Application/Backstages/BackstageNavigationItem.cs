using CrossApplication.Core.Wpf.Contracts.Backstages;

namespace CrossApplication.Wpf.Application.Backstages
{
    public class BackstageNavigationItem : IBackstageNavigationItem
    {
        public BackstageNavigationItem(string label, string navigationKey)
        {
            Label = label;
            NavigationKey = navigationKey;
        }

        public string Label { get; }
        public string NavigationKey { get; }
    }
}