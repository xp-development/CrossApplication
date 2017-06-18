using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Views;
using Prism.Mvvm;
using Prism.Navigation;

namespace CrossApplication.Core.About
{
    public class AboutViewModel : BindableBase, IViewActivatedAsync
    {
        public Version Version
        {
            get => _version;
            set
            {
                _version = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<ModuleInfo> ModuleInfos { get; } = new ObservableCollection<ModuleInfo>();

        public AboutViewModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            Version = await _aboutService.GetVersionAsync();
            ModuleInfos.Clear();
            foreach (var moduleInfo in await _aboutService.GetModuleInfosAsync())
                ModuleInfos.Add(moduleInfo);
        }

        private readonly IAboutService _aboutService;
        private Version _version;
    }
}