using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Photos.Contracts;
using Prism.Commands;
using Prism.Events;

namespace CrossApplication.Photos.Wpf.Shell
{
    public class ShellViewModel : ViewModelBase, IViewActivatedAsync, IViewDeactivatedAsync
    {
        public string DirectoryToBackup
        {
            get => _directoryToBackup;
            set
            {
                _directoryToBackup = value;
                NotifyPropertyChanged();
            }
        }

        public string BackupTargetDirectory
        {
            get => _backupTargetDirectory;
            set
            {
                _backupTargetDirectory = value;
                NotifyPropertyChanged();
            }
        }

        public DelegateCommand BackupCommand { get; }

        public ShellViewModel(IStorage storage, IPhotoBackupService backupService, IEventAggregator eventAggregator)
        {
            _storage = storage;
            _backupService = backupService;
            _stateMessageEvent = eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>();
            BackupCommand = new DelegateCommand(OnBackupAsync);
        }

        public async Task OnViewActivatedAsync(NavigationParameters navigationParameters)
        {
            DirectoryToBackup = await _storage.ReadAsync<string>(nameof(DirectoryToBackup));
            BackupTargetDirectory = await _storage.ReadAsync<string>(nameof(BackupTargetDirectory));
        }

        public async Task OnViewDeactivatedAsync()
        {
            await _storage.SaveAsync(nameof(DirectoryToBackup), DirectoryToBackup);
            await _storage.SaveAsync(nameof(BackupTargetDirectory), BackupTargetDirectory);
        }

        private async void OnBackupAsync()
        {
            _stateMessageEvent.Publish(new StateMessageEvent("Backups running..."));
            await _backupService.BackupAsync(DirectoryToBackup, BackupTargetDirectory);
            _stateMessageEvent.Publish(new StateMessageEvent("Backups finished."));
        }

        private readonly IPhotoBackupService _backupService;
        private readonly IStorage _storage;
        private string _backupTargetDirectory;
        private string _directoryToBackup;
        private readonly PubSubEvent<StateMessageEvent> _stateMessageEvent;
    }
}