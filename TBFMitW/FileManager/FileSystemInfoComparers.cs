using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    public class DirFilesComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo x, FileSystemInfo y)
        {
            if (x.ToDirectoryInfo().Exists && y.ToFileInfo().Exists)
                return -1;
            else if (x.ToFileInfo().Exists && y.ToDirectoryInfo().Exists)
                return 1;
            else
                return 0;
        }
    }

    public class LastWriteDateComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo x, FileSystemInfo y) => x.LastWriteTime.CompareTo(y.LastWriteTime);
    }

    public class NameComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo x, FileSystemInfo y) => x.Name.CompareTo(y.Name);
    }

    public class FileSizeComparer : IComparer<FileSystemInfo>
    {
        public int Compare(FileSystemInfo x, FileSystemInfo y)
        {
            if (x.ToFileInfo().Exists && y.ToFileInfo().Exists && x.ToFileInfo().Length > y.ToFileInfo().Length)
                return 1;
            else if (x.ToFileInfo().Exists && y.ToFileInfo().Exists && x.ToFileInfo().Length < y.ToFileInfo().Length)
                return -1;
            else if (x.ToFileInfo().Exists && y.ToDirectoryInfo().Exists)
                return -1;
            else if (x.ToDirectoryInfo().Exists && y.ToFileInfo().Exists)
                return 1;
            else
                return 0;
        }
    }
}
