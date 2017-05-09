using System.Reactive.Subjects;

namespace DropBoxClient
{
    public class DroppedFileWatcher
    {
        private string path;

        private ObservableSystemFilesCollectionsProvider fileWatcher;
        private readonly Subject<SubjectInfo> subject = new Subject<SubjectInfo>();
        public DroppedFileWatcher(ObservableSystemFilesCollectionsProvider _fileWatcher, string _path)
        {
            path = _path;
            fileWatcher = _fileWatcher;
        }

        

    }
}
