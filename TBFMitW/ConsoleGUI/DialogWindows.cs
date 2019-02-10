using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FileManager;

namespace ConsoleGUI
{
    class DialogWindows : IDialogWindows
    {
        void Base(int commonDivider, int topLeftx, int topLefty, int bottomRightx, int bottomRighty)
        {
            int width = bottomRightx - topLeftx;
            int height = bottomRighty - topLefty;
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = Console.WindowHeight / commonDivider * topLefty; i < Console.WindowHeight / commonDivider * bottomRighty; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / commonDivider * topLeftx, i);
                Console.Write('║');
                Console.SetCursorPosition(Console.WindowWidth / commonDivider * bottomRightx, i);
                Console.Write('║');
            }
            for (int i = Console.WindowWidth / commonDivider * topLeftx; i < Console.WindowWidth / commonDivider * bottomRightx; i++)
            {
                Console.SetCursorPosition(i, Console.WindowHeight / commonDivider * topLefty);
                Console.Write('═');
                Console.SetCursorPosition(i, Console.WindowHeight / commonDivider * bottomRighty);
                Console.Write('═');
            }
            Console.SetCursorPosition(Console.WindowWidth / commonDivider * topLeftx, Console.WindowHeight / commonDivider * topLefty);
            Console.Write('╔');
            Console.SetCursorPosition(Console.WindowWidth / commonDivider * bottomRightx, Console.WindowHeight / commonDivider * topLefty);
            Console.Write('╗');
            Console.SetCursorPosition(Console.WindowWidth / commonDivider * topLeftx, Console.WindowHeight / commonDivider * bottomRighty);
            Console.Write('╚');
            Console.SetCursorPosition(Console.WindowWidth / commonDivider * bottomRightx, Console.WindowHeight / commonDivider * bottomRighty);
            Console.Write("╝");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = Console.WindowHeight / commonDivider * topLefty + 1; i < Console.WindowHeight / commonDivider * bottomRighty; i++)
            {
                for (int j = Console.WindowWidth / commonDivider * topLeftx + 1; j < Console.WindowWidth / commonDivider * bottomRightx; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.Write(' ');
                }
            }
        }

        void PrintMsg(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string[] lol = msg.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int symbols = 0;
            int line = 0;
            CursorControl.DialogWindowPosition();
            foreach (var i in lol)
            {

                if (i.ContainsOneOf(Environment.NewLine.ToArray()))
                {
                    foreach (var s in i)
                    {
                        if (s.EqualToOneOf(Environment.NewLine.ToArray()))
                        {
                            if (symbols != 0)
                            {
                                CursorControl.DialogWindowPosition(++line);
                                symbols = 0;
                            }
                            continue;
                        }
                        Console.Write(s);
                        symbols++;
                    }
                    Console.Write(' ');
                    continue;
                }
                else if (symbols + i.Length >= Console.WindowWidth / 2 - Console.WindowWidth / 10)
                {
                    symbols = 0;
                    CursorControl.DialogWindowPosition(++line);
                }
                symbols += i.Length + 1;
                Console.Write(i + ' ');
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Message(string msg)
        {
            Base(4, 1, 1, 3, 3);
            PrintMsg(msg);
            Console.ForegroundColor = ConsoleColor.Red;
            CursorControl.DialogWindowPosition(Console.WindowHeight / 2 - Console.WindowHeight / 8);
            Console.Write("OK".PadLeft((Console.WindowWidth / 2 - Console.WindowWidth / 10) / 2));
            Console.ForegroundColor = ConsoleColor.White;
            ConsoleKeyInfo k = new ConsoleKeyInfo();
            while (k.Key != ConsoleKey.Enter)
                k = Console.ReadKey(true);
        }

        public string Input(string purpose)
        {
            Base(4, 1, 1, 3, 3);
            PrintMsg(purpose);
            ConsoleKeyInfo k = new ConsoleKeyInfo();
            string res = String.Empty;
            CursorControl.DialogWindowPosition(Console.WindowHeight / 8);
            Console.CursorVisible = true;
            int line = 0;
            while (k.Key != ConsoleKey.Enter)
            {
                k = Console.ReadKey(true);
                if (!char.IsControl(k.KeyChar) && line < Console.WindowHeight / 2 - Console.WindowHeight / 8 - 3)
                {
                    res += k.KeyChar;
                    Console.Write(k.KeyChar);
                    if (res.Length % (Console.WindowWidth / 2 - Console.WindowWidth / 10) == 0)
                    {
                        line++;
                        CursorControl.DialogWindowPosition(Console.WindowHeight / 8 + line);
                    }
                }
                else if (k.Key == ConsoleKey.Backspace && res.Length > 0)
                {

                    if (res.Length % (Console.WindowWidth / 2 - Console.WindowWidth / 10) == 0)
                    {
                        line--;
                        CursorControl.DialogWindowEndPosition(Console.WindowHeight / 8 + line);
                        Console.CursorLeft--;
                    }
                    else
                        Console.CursorLeft--;
                    res = res.Remove(res.Length - 1, 1);
                    Console.Write(" ");
                    Console.CursorLeft--;
                }
                else if (k.Key == ConsoleKey.Escape)
                {
                    res = string.Empty;
                    break;
                }
            }
            Console.CursorVisible = false;
            return res;
        }

        public bool Confirmation(string msg)
        {
            Base(4, 1, 1, 3, 3);
            PrintMsg(msg);
            ConsoleKeyInfo k = new ConsoleKeyInfo();
            bool yes = true;
            while (k.Key != ConsoleKey.Enter)
            {
                if (k.Key == ConsoleKey.LeftArrow || k.Key == ConsoleKey.RightArrow)
                    yes = !yes;
                CursorControl.DialogWindowPosition(Console.WindowHeight / 2 - Console.WindowHeight / 8);
                Console.ForegroundColor = yes ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("YES".PadLeft(Console.WindowWidth / 8));
                Console.ForegroundColor = yes ? ConsoleColor.White : ConsoleColor.Red;
                Console.Write("NO".PadLeft(Console.WindowWidth / 6));
                k = Console.ReadKey(true);
            }
            Console.ForegroundColor = ConsoleColor.White;
            return yes;
        }

        public void Loading(CancellationToken token)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Base(40, 17, 19, 23, 21);
            CursorControl.LoadingPosition();
            Console.Write("Loading");
            int c = 0;
            while (true)
            {
                if (token.IsCancellationRequested)
                    break;
                Thread.Sleep(500);
                if (token.IsCancellationRequested)
                    break;
                if (c == 3)
                {
                    Console.CursorLeft -= 3;
                    Console.Write("   ");
                    Console.CursorLeft -= 3;
                    c = 0;
                }
                else
                {
                    Console.Write('.');
                    c++;
                }
            }
        }

        public void SystemEntryInfoMsg(FileSystemInfo fsi)
        {
            try
            {
                if (fsi.Is() == FileSystemEntry.Directory)
                {
                    CancellationTokenSource tokenSource = new CancellationTokenSource();
                    Task loading = new Task(() => Loading(tokenSource.Token));
                    loading.Start();
                    DirectoryInfo d = fsi.ToDirectoryInfo();
                    string size = d.GetSize();
                    var counts = d.GetAllDirAndFilesCount();
                    tokenSource.Cancel();
                    loading.Wait();
                    Base(4, 1, 1, 3, 3);
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
                    Base(4, 1, 1, 3, 3);
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
                    Base(4, 1, 1, 3, 3);
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

        public void ErrorMessage(string msg) => Message(msg);
    }

}
