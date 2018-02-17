using System.Collections.Generic;
using System.Collections.ObjectModel;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using Prism.Commands;

namespace CrossApplication.Modules.Settings.Wpf.Shell
{
    public class ShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public DelegateCommand CloseCommand { get; }

        public ShellViewModel(INavigationService navigationService, IEnumerable<ISettingsNavigationItem> settingsNavigationItems)
        {
            _navigationService = navigationService;

            CloseCommand = new DelegateCommand(OnClose);

            foreach (var settingsNavigationItem in settingsNavigationItems)
                NavigationItems.Add(new NavigationItem(_navigationService, settingsNavigationItem.Label, settingsNavigationItem.NavigationKey, settingsNavigationItem.Glyph));
        }

        private async void OnClose()
        {
            await _navigationService.NavigateBackAsync();
        }

        private readonly INavigationService _navigationService;
    }
}