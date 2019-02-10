using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileManager;
using System.Threading;
using System.IO;

namespace WinFormsGUI
{
    class DialogWindows : IDialogWindows
    {
        Input InputForm;
        public DialogWindows() {  InputForm = new Input(this); }
        public string ResString { get; set; } = string.Empty;
        public bool Confirmation(string msg)
        {
            var res = MessageBox.Show(msg, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public void ErrorMessage(string msg) => MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public string Input(string purpose)
        {
            InputForm.SetText(purpose);
            InputForm.ShowDialog();
            return ResString;
        }


        public void Message(string msg) => MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void SystemEntryInfoMsg(FileSystemInfo fsi)
        {
            try
            {
                if (fsi.Is() == FileSystemEntry.Directory)
                {
                    DirectoryInfo d = fsi.ToDirectoryInfo();
                    string size = d.GetSize();
                    var counts = d.GetAllDirAndFilesCount();
                    Message($"Name - {d.FullName}{Environment.NewLine}" +
                        $"Type - Directory{Environment.NewLine}" +
                        $"Size - {size}{Environment.NewLine}" +
                        $"Creation Time - {d.CreationTime}{Environment.NewLine}" +
                        $"Last Write Time - {d.LastWriteTime}{Environment.NewLine}" +
                        $"Last Access Time - {d.LastAccessTime}{Environment.NewLine}" +
                        $"Directories Count - {d.GetDirectories().Length}{Environment.NewLine}" +
                        $"Files Count - {d.GetFiles().Length}{Environment.NewLine}" +
                        $"Including subdirectories:{Environment.NewLine}" +
                        $"----Directories Count - {counts[FileSystemEntry.Directory]}{Environment.NewLine}" +
                        $"----Files Count - {counts[FileSystemEntry.File]}");
                }
                else if (fsi.Is() == FileSystemEntry.File)
                {
                    FileInfo f = fsi.ToFileInfo();
                    Message($"Name - {f.FullName}{Environment.NewLine}" +
                        $"Type - File({f.Extension}){Environment.NewLine}" +
                        $"Size - {f.GetSize()}{Environment.NewLine}" +
                        $"Creation Time - {f.CreationTime}{Environment.NewLine}" +
                        $"Last Write Time - {f.LastWriteTime}{Environment.NewLine}" +
                        $"Last Access Time - {f.LastAccessTime}{Environment.NewLine}" +
                        $"Attributes - {f.Attributes}{Environment.NewLine}");
                }
                else
                {
                    DriveInfo drive = fsi.ToDriveInfo();
                    Message($"Name - {drive.Name}{Environment.NewLine}" +
                        $"Type - Drive({drive.DriveType}){Environment.NewLine}" +
                        $"Total Size - {fsi.ConvertSize(drive.TotalSize)}{Environment.NewLine}" +
                        $"Total Available Free Space - {fsi.ConvertSize(drive.TotalFreeSpace)}{Environment.NewLine}" +
                        $"File System - {drive.DriveFormat}");
                }
            }
            catch (Exception e)
            {
                ErrorMessage(e.Message);
            }
        }
    }
}
