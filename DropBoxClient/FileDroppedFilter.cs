namespace DropBoxClient
{
    public interface IFileDroppedFilter
    {
        bool Filter(string fileName);
    }

    public class NoneFileDroppedFilter : IFileDroppedFilter
    {
        private static NoneFileDroppedFilter filter = null;

        public NoneFileDroppedFilter()
        {

        }
        public bool Filter(string filePath)
        {
            return true;
        }

        public  static IFileDroppedFilter GetFilter()
        {
            if (filter == null)
                filter = new NoneFileDroppedFilter();
            return filter;
        }
    }

    public class FileDroppedFilter : IFileDroppedFilter
    {
        FromServerListProvider filesFromServer;

        public FileDroppedFilter(FromServerListProvider filesFromServer)
        {
            this.filesFromServer = filesFromServer;
        }

        public bool Filter(string filePath)
        {
            return !filesFromServer.GetFiles().Contains(filePath.Substring(filePath.LastIndexOf("\\")+1));
        }
    }
}
