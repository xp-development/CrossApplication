using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CrossApplication.Core.Contracts.Application.Events;
using CrossApplication.Core.Contracts.Events;
using CrossApplication.Photos.Contracts;

namespace CrossApplication.Photos.Core.Services
{
    public class PhotoBackupService : IPhotoBackupService
    {
        public PhotoBackupService(IEventAggregator eventAggregator)
        {
            _stateMessageEvent = eventAggregator.GetEvent<StateMessageEventPayload>();
            _progressMessageEvent = eventAggregator.GetEvent<ProgressMessageEventPayload>();
        }

        public async Task BackupAsync(string directoryToBackup, string backupTargetDirectory)
        {
            foreach (var directory in Directory.GetDirectories(directoryToBackup))
            {
                var destinationArchiveFileName = Path.Combine(backupTargetDirectory, Path.GetFileName(directory) + ".zip");

                if (!File.Exists(destinationArchiveFileName))
                {
                    await _stateMessageEvent.PublishAsync(new StateMessageEventPayload($"Backup {destinationArchiveFileName}..."));
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
                                await _progressMessageEvent.PublishAsync(new ProgressMessageEventPayload((int)(100d / count * ++i)));
                            }
                        }
                    }

                    await _stateMessageEvent.PublishAsync(new StateMessageEventPayload($"Backup {destinationArchiveFileName} created."));
                }
                else
                {
                    using (var fileStream = new FileStream(destinationArchiveFileName, FileMode.OpenOrCreate))
                    {
                        await _stateMessageEvent.PublishAsync(new StateMessageEventPayload($"Check and update backup {destinationArchiveFileName}..."));
                        using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Update))
                        {
                            var changed = false;
                            var fullFileNames = Directory.EnumerateFiles(directory).ToList();
                            var count = fullFileNames.Count;
                            var i = 0;
                            foreach (var fullFileName in fullFileNames)
                            {
                                await _progressMessageEvent.PublishAsync(new ProgressMessageEventPayload((int)(100d / count * ++i)));
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

                                await _stateMessageEvent.PublishAsync(new StateMessageEventPayload($"Backup {destinationArchiveFileName} updated."));
                            }
                        }
                    }
                }
                await _progressMessageEvent.PublishAsync(new ProgressMessageEventPayload(100));
            }
        }

        private readonly IEvent<ProgressMessageEventPayload> _progressMessageEvent;
        private readonly IEvent<StateMessageEventPayload> _stateMessageEvent;
    }
}