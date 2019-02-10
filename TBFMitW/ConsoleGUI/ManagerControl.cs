using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileManager;

namespace ConsoleGUI
{
    class ManagerControl
    {

        public bool MainActive { get; private set; } = true;
        public Manager manager;
        public bool NoNeedScreenUpdate { get; set; } = false;
        public ManagerControl(Manager m ){ manager = m; }

        public void MenuControl()
        {
            if (ConsoleHelpers.Key.Key == ConsoleKey.Tab)
            {
                NoNeedScreenUpdate = true;
                MainActive = !MainActive;
            }
            if (MainActive)
                MenuAction();
            else
                AddMenuAction();
        }

        void MenuAction()
        {
            Task loadingTask = null;
            CancellationTokenSource tokenSource = null;
            switch (ConsoleHelpers.Key.Key)
            {
                case ConsoleKey.UpArrow:
                    manager.MainMenu.MoveUp();
                    NoNeedScreenUpdate = true;
                    break;
                case ConsoleKey.DownArrow:
                    manager.MainMenu.MoveDown();
                    NoNeedScreenUpdate = true;
                    break;
                case ConsoleKey.Enter:
                    manager.MoveIn(manager.MainMenu);
                    break;
                case ConsoleKey.Backspace:
                    manager.MoveOut();
                    break;
                case ConsoleKey.S:
                    string pattern = manager.ManagerDialogWindows.Input("Input search pattern");
                    tokenSource = new CancellationTokenSource();
                    loadingTask = new Task(() => (manager.ManagerDialogWindows as DialogWindows).Loading(tokenSource.Token));
                    loadingTask.Start();
                    manager.Search(pattern);
                    tokenSource.Cancel();
                    loadingTask.Wait();
                    break;
                case ConsoleKey.F9:
                    manager.LastSearchRes();
                    break;
                case ConsoleKey.F10:
                    manager.ClearLog();
                    break;
                case ConsoleKey.H:
                    manager.History();
                    break;
                case ConsoleKey.R:
                    manager.Rename();
                    break;
                case ConsoleKey.D:
                    manager.CreateDirectory();
                    break;
                case ConsoleKey.Delete:
                    manager.Delete();
                    break;
                case ConsoleKey.F:
                    manager.CreateFile();
                    break;
                case ConsoleKey.C:
                    manager.CopyFrom();
                    break;
                case ConsoleKey.V:
                    tokenSource = new CancellationTokenSource();
                    loadingTask = new Task(() => (manager.ManagerDialogWindows as DialogWindows).Loading(tokenSource.Token));
                    loadingTask.Start();
                    manager.Paste();
                    tokenSource.Cancel();
                    loadingTask.Wait();
                    break;
                case ConsoleKey.X:
                    manager.MoveFrom();
                    break;
                case ConsoleKey.P:
                    manager.SetPath();
                    break;
                case ConsoleKey.F2:
                    manager.SelectedInfo();
                    break;
                case ConsoleKey.F3:
                    manager.CurrentDirInfo();
                    break;
                case ConsoleKey.F1:
                    manager.Help("Help.txt");
                    NoNeedScreenUpdate = true;
                    break;
                case ConsoleKey.F5:
                    manager.Sort(new NameComparer());
                    break;
                case ConsoleKey.F6:
                    manager.Sort(new DirFilesComparer());
                    break;
                case ConsoleKey.F7:
                    manager.Sort(new LastWriteDateComparer());
                    break;
                case ConsoleKey.F8:
                    manager.Sort(new FileSizeComparer());
                    break;
                case ConsoleKey.F4:
                    manager.MakeDescending();
                    break;
                default:
                    NoNeedScreenUpdate = true;
                    break;
            }
        }
        void AddMenuAction()
        {
            switch (ConsoleHelpers.Key.Key)
            {
                case ConsoleKey.UpArrow:
                    manager.AddMenu.MoveUp();
                    NoNeedScreenUpdate = true;
                    break;
                case ConsoleKey.DownArrow:
                    manager.AddMenu.MoveDown();
                    NoNeedScreenUpdate = true;
                    break;
                case ConsoleKey.Enter:
                    manager.MoveIn(manager.AddMenu);
                    break;
                default:
                    NoNeedScreenUpdate = true;
                    break;
            }
        }

    }
}
