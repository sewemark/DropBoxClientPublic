using System;
using System.IO;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace DropBoxClient
{
    public class DroppedFileWatcher
    {
        private string path;
        private string filter;
        private ObservableSystemFilesCollectionsProvider fileWatcher;
        public IObservable<SubjectInfo> DirectoryDiff { get; private set; }
        private readonly Subject<SubjectInfo> subject = new Subject<SubjectInfo>();

        public DroppedFileWatcher( string _path,string _filter)
        {
            path = _path;
            filter = _filter;
            FileSystemWatcher watcher = new FileSystemWatcher();
           
            watcher.Filter = filter;
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
             
            fileWatcher = new ObservableSystemFilesCollectionsProvider(watcher);
            
            DirectoryDiff = fileWatcher.Created.Select(x => new SubjectInfo(x));
        }

        public void StartWatching()
        {
            fileWatcher.StartProviding();
        }
    }
}
