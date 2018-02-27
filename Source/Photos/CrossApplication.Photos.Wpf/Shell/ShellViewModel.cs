using System.Threading.Tasks;
using CrossApplication.Core.Common.Mvvm;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Common.Navigation;
using CrossApplication.Core.Contracts.Common.Storage;
using CrossApplication.Core.Contracts.Events;
using CrossApplication.Core.Contracts.Views;
using CrossApplication.Photos.Contracts;

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

        public DelegateCommand<object, object> BackupCommand { get; }

        public ShellViewModel(IStorage storage, IPhotoBackupService backupService, IEventAggregator eventAggregator)
        {
            _storage = storage;
            _backupService = backupService;
            _stateMessageEvent = eventAggregator.GetEvent<StateMessageEventPayload>();
            BackupCommand = new DelegateCommand<object, object>(OnBackupAsync);
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

        private async Task OnBackupAsync(object args)
        {
            await _stateMessageEvent.PublishAsync(new StateMessageEventPayload("Backups running..."));
            await _backupService.BackupAsync(DirectoryToBackup, BackupTargetDirectory);
            await _stateMessageEvent.PublishAsync(new StateMessageEventPayload("Backups finished."));
        }

        private readonly IPhotoBackupService _backupService;
        private readonly IStorage _storage;
        private string _backupTargetDirectory;
        private string _directoryToBackup;
        private readonly IEvent<StateMessageEventPayload> _stateMessageEvent;
    }
}