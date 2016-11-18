using System.Collections.ObjectModel;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Wpf.Common.Navigation;

namespace CrossApplication.Mail.Wpf.Navigation
{
    public class NavigationViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public NavigationViewModel(INavigationService navigationService)
        {
            NavigationItems.Add(new NavigationItem(navigationService, "Input", ""));
            NavigationItems.Add(new NavigationItem(navigationService, "Output", ""));
        }
    }
}