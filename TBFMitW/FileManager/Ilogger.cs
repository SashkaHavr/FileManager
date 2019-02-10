using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    public interface ILogger
    {
        void Log(string actionType, string msg);
        List<FileSystemInfo> History();
        void ClearLog();
    }
}
