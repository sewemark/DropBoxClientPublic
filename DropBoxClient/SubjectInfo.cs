using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DropBoxClient
{
    public class SubjectInfo
    {
        private FileSystemEventArgs x;

        public SubjectInfo(FileSystemEventArgs x)
        {
            this.x = x;
        }
        public string FilePath
        {
            get
            {
                return x.FullPath;
            }
        }
    }
}
