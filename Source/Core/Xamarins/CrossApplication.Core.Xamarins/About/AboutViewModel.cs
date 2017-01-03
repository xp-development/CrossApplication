using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Modules;
using CrossApplication.Core.Contracts.Application.Services;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Core.Resources;
using Prism.Mvvm;

namespace CrossApplication.Core.Xamarins.About
{
    public class AboutViewModel : BindableBase, IViewLoadingAsync
    {
        public Version Version
        {
            get { return _version; }
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ModuleInfo> ModuleInfos { get; } = new ObservableCollection<ModuleInfo>();

        public AboutViewModel(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        public async Task OnViewLoadingAsync()
        {
            Version = await _aboutService.GetVersionAsync();

            ModuleInfos.Clear();
            foreach (var moduleInfo in await _aboutService.GetModuleInfosAsync())
            {
                ModuleInfos.Add(moduleInfo);
            }
        }

        private readonly IAboutService _aboutService;
        private Version _version;
    }
}