using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Views;
using Prism.Navigation;
using INavigationService = CrossApplication.Core.Contracts.Common.Navigation.INavigationService;

namespace CrossApplication.Core.Xamarins.Shell
{
    public class RichShellViewModel : IViewActivatedAsync
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public RichShellViewModel(IEnumerable<IMainNavigationItem> mainNavigationItems, INavigationService navigationService)
        {
            _mainNavigationItems = mainNavigationItems;
            _navigationService = navigationService;
        }

        public Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            foreach (var mainNavigationItem in _mainNavigationItems)
            {
                NavigationItems.Add(new NavigationItem(_navigationService, mainNavigationItem.Label, mainNavigationItem.NavigationKey));
            }

            return Task.FromResult(false);
        }

        private readonly IEnumerable<IMainNavigationItem> _mainNavigationItems;
        private readonly INavigationService _navigationService;
    }
}