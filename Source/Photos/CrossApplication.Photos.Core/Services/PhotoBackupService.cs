using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Photos.Contracts;
using Prism.Events;

namespace CrossApplication.Photos.Core.Services
{
    public class PhotoBackupService : IPhotoBackupService
    {
        private PubSubEvent<StateMessageEvent> _stateMessageEvent;

        public PhotoBackupService(IEventAggregator eventAggregator)
        {
            _stateMessageEvent = eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>();
        }

        public Task BackupAsync(string directoryToBackup, string backupTargetDirectory)
        {
            return Task.Run(() =>
            {
                foreach (var directory in Directory.GetDirectories(directoryToBackup))
                {
                    var destinationArchiveFileName = Path.Combine(backupTargetDirectory, Path.GetFileName(directory) + ".zip");

                    if (!File.Exists(destinationArchiveFileName))
                    {
                        _stateMessageEvent.Publish(new StateMessageEvent($"Backup {destinationArchiveFileName}..."));
                        using (var fileStream = new FileStream(destinationArchiveFileName, FileMode.OpenOrCreate))
                        {
                            using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                            {
                                var reportEntry = zipArchive.CreateEntry("Report.txt");
                                using (var writer = new StreamWriter(reportEntry.Open()))
                                {
                                    writer.WriteLine($"Created {DateTime.Now:s}");
                                }

                                foreach (var fullFileName in Directory.EnumerateFiles(directory))
                                {
                                    zipArchive.CreateEntryFromFile(fullFileName, Path.GetFileName(fullFileName));
                                }
                            }
                        }
                        _stateMessageEvent.Publish(new StateMessageEvent($"Backup {destinationArchiveFileName} erstellt."));
                    }
                    else
                    {
                        using (var fileStream = new FileStream(destinationArchiveFileName, FileMode.OpenOrCreate))
                        {
                            _stateMessageEvent.Publish(new StateMessageEvent($"Überprüfe Backup {destinationArchiveFileName}..."));
                            using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Update))
                            {
                                var changed = false;
                                foreach (var fullFileName in Directory.EnumerateFiles(directory))
                                {
                                    var fileName = Path.GetFileName(fullFileName);
                                    if (zipArchive.Entries.Any(x => x.Name == fileName))
                                        continue;

                                    changed = true;
                                    zipArchive.CreateEntryFromFile(fullFileName, Path.GetFileName(fullFileName));
                                }

                                if (changed)
                                {
                                    var reportEntry = zipArchive.GetEntry("Report.txt");
                                    using (var writer = new StreamWriter(reportEntry.Open()))
                                    {
                                        writer.BaseStream.Position = writer.BaseStream.Length;
                                        writer.WriteLine($"Edited {DateTime.Now:s}");
                                    }

                                    _stateMessageEvent.Publish(new StateMessageEvent($"Backup {destinationArchiveFileName} aktualisiert."));
                                }
                            }
                        }
                    }
                }
            });
        }
    }
}