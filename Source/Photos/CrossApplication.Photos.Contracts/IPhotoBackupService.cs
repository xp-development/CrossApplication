using System.Threading.Tasks;

namespace CrossApplication.Photos.Contracts
{
    public interface IPhotoBackupService
    {
        Task BackupAsync(string directoryToBackup, string backupTargetDirectory);
    }
}