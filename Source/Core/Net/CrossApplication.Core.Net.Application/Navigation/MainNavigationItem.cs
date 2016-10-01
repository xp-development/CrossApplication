using CrossApplication.Core.Net.Contracts.Navigation;

namespace CrossApplication.Core.Net.Application.Navigation
{
    public class MainNavigationItem : IMainNavigationItem
    {
        public string Label { get; }
        public string NavigationKey { get; }

        public MainNavigationItem(string label, string navigationKey)
        {
            Label = label;
            NavigationKey = navigationKey;
        }
    }
}