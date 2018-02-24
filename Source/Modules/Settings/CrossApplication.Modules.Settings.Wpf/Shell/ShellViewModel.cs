using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Navigation;
using CrossApplication.Core.Contracts.Settings;

namespace CrossApplication.Modules.Settings.Wpf.Shell
{
    public class ShellViewModel
    {
        public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>();

        public DelegateCommand<object, object> SaveCommand { get; }
        public DelegateCommand<object, object> CloseCommand { get; }

        public ShellViewModel(INavigationService navigationService, IEnumerable<ISettingsNavigationItem> settingsNavigationItems, IEnumerable<ISettingsChild> settingsChilds)
        {
            _navigationService = navigationService;
            _settingsChilds = settingsChilds;

            SaveCommand = new DelegateCommand<object, object>(OnSave);
            CloseCommand = new DelegateCommand<object, object>(OnClose);

            foreach (var settingsNavigationItem in settingsNavigationItems)
                NavigationItems.Add(new NavigationItem(_navigationService, settingsNavigationItem.Label, settingsNavigationItem.NavigationKey, settingsNavigationItem.Glyph));
        }

        private async Task OnSave(object args)
        {
            foreach (var settingsChild in _settingsChilds)
                await settingsChild.SaveAsync();

            await _navigationService.NavigateBackAsync();
        }

        private async Task OnClose(object args)
        {
            await _navigationService.NavigateBackAsync();
        }

        private readonly INavigationService _navigationService;
        private readonly IEnumerable<ISettingsChild> _settingsChilds;
    }
}