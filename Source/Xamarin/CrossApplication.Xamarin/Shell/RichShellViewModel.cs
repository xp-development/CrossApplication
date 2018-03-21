using System.Collections.Generic;
using System.Collections.ObjectModel;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;

namespace CrossApplication.Xamarin.Shell
{
    public class RichShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public RichShellViewModel(IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService)
        {
            foreach (var mainNavigationItem in mainNavigationItems)
                NavigationItems.Add(new NavigationItem(navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey, mainNavigationItem.Glyph));
        }
    }
}
