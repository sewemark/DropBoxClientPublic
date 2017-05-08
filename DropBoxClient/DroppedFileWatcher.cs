using System.Reactive.Subjects;

namespace DropBoxClient
{
    public class DroppedFileWatcher
    {
        private string path;

        private FileWatcher fileWatcher;
        private readonly Subject<SubjectInfo> subject = new Subject<SubjectInfo>();
        public DroppedFileWatcher(FileWatcher _fileWatcher, string _path)
        {
            path = _path;
            fileWatcher = _fileWatcher;
        }

        

    }
}
