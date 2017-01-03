using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Core.Common.Navigation
{
    public class MainNavigationItem : IMainNavigationItem
    {
        public MainNavigationItem(string label, string navigationKey)
        {
            Label = label;
            NavigationKey = navigationKey;
        }

        public string Label { get; }
        public string NavigationKey { get; }
    }
}