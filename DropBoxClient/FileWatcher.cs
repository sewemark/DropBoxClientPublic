using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DropBoxClient
{
    public class FileWatcher : IDisposable
    {
        private FileSystemWatcher fileSystemWatcher;
        public FileWatcher(FileSystemWatcher _fileSystemWatcher)
        {
            fileSystemWatcher = _fileSystemWatcher;
        }
        public void Dispose()
        {
            fileSystemWatcher.Dispose();
        }
    }
}
