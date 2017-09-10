using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reactive.Linq;
using System.Reactive.Subjects;
namespace DropBoxClient
{
    public class ObservableSystemFilesCollectionsProvider : IDisposable
    {
        private FileSystemWatcher fileSystemWatcher;
        public IObservable<FileSystemEventArgs> Created { get; set; }
        public ObservableSystemFilesCollectionsProvider(FileSystemWatcher _fileSystemWatcher)
        {
                
            fileSystemWatcher = _fileSystemWatcher;
            Created = Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>
                    (target => fileSystemWatcher.Created += target, eventName => fileSystemWatcher.Created -= eventName)
                    .Select(x => x.EventArgs);

        }

        public void StartProviding()
        {
            fileSystemWatcher.EnableRaisingEvents = true;
        }

        public void StopProviding()
        {
            fileSystemWatcher.EnableRaisingEvents = false;
        }
        
        public void Dispose()
        {
            fileSystemWatcher.Dispose();
        }
    }
}
