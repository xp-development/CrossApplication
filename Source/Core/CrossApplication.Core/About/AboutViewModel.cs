using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Views;

namespace CrossApplication.Core.About
{
    public class AboutViewModel : ViewModelBase, IViewActivatedAsync
    {
        public Version Version
        {
            get => _version;
            set
            {
                _version = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> ModuleInfos { get; } = new ObservableCollection<string>();

        public AboutViewModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            Version = await _aboutService.GetVersionAsync();
            ModuleInfos.Clear();
            foreach (var moduleInfo in await _aboutService.GetModuleInfosAsync())
                ModuleInfos.Add(moduleInfo.ModuleType.Name);
        }

        private readonly IAboutService _aboutService;
        private Version _version;
    }
}