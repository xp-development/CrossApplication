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
        public PhotoBackupService(IEventAggregator eventAggregator)
        {
            _stateMessageEvent = eventAggregator.GetEvent<PubSubEvent<StateMessageEvent>>();
            _progressMessageEvent = eventAggregator.GetEvent<PubSubEvent<ProgressMessageEvent>>();
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

                                var fullFileNames = Directory.EnumerateFiles(directory).ToList();
                                var count = fullFileNames.Count;
                                var i = 0;
                                foreach (var fullFileName in fullFileNames)
                                {
                                    zipArchive.CreateEntryFromFile(fullFileName, Path.GetFileName(fullFileName));
                                    _progressMessageEvent.Publish(new ProgressMessageEvent((int)(100d / count * ++i)));
                                }
                            }
                        }

                        _stateMessageEvent.Publish(new StateMessageEvent($"Backup {destinationArchiveFileName} created."));
                    }
                    else
                    {
                        using (var fileStream = new FileStream(destinationArchiveFileName, FileMode.OpenOrCreate))
                        {
                            _stateMessageEvent.Publish(new StateMessageEvent($"Check and update backup {destinationArchiveFileName}..."));
                            using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Update))
                            {
                                var changed = false;
                                var fullFileNames = Directory.EnumerateFiles(directory).ToList();
                                var count = fullFileNames.Count;
                                var i = 0;
                                foreach (var fullFileName in fullFileNames)
                                {
                                    _progressMessageEvent.Publish(new ProgressMessageEvent((int)(100d / count * ++i)));
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

                                    _stateMessageEvent.Publish(new StateMessageEvent($"Backup {destinationArchiveFileName} updated."));
                                }
                            }
                        }
                    }
                    _progressMessageEvent.Publish(new ProgressMessageEvent(100));
                }
            });
        }

        private readonly PubSubEvent<ProgressMessageEvent> _progressMessageEvent;
        private readonly PubSubEvent<StateMessageEvent> _stateMessageEvent;
    }
}