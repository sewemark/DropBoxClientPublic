using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reactive.Linq;
using System.Reactive.Subjects;
namespace DropBoxClient
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher fileSystemWatcher;
        public IObservable<FileSystemEventArgs> Created { get; set; }
        public FileWatcher(FileSystemWatcher _fileSystemWatcher)
        {
            
            fileSystemWatcher = _fileSystemWatcher;
            //Created = Obser
        }
        public void Dispose()
        {
            fileSystemWatcher.Dispose();
        }
    }
}
