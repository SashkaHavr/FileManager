using System.IO;
using System.Threading;
namespace FileManager
{
    public interface IDialogWindows
    {
        void Message(string msg);
        void ErrorMessage(string msg);
        string Input(string purpose);
        bool Confirmation(string msg);
        void SystemEntryInfoMsg(FileSystemInfo systemEntry);
    }
}
