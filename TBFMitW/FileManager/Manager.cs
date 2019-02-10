using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FileManager
{
    public class Manager
    {
        public DirectoryInfo CurrentDirectory { get; private set; }
        public Menu MainMenu { get; }
        public Menu AddMenu { get; }
        public List<FileSystemInfo> SearchRes { get; private set; } = new List<FileSystemInfo>();
        public State ContentState { get; private set; } = State.Default;
        public IDialogWindows ManagerDialogWindows { get; }
        ILogger ManagerLogger { get; }
        public string CopyPath { get; private set; } = String.Empty;
        public bool IsCopy { get; private set; } = false;

        public Manager(IDialogWindows dialog, ILogger logger, Menu main, Menu add)
        {
            CurrentDirectory = new DirectoryInfo(DriveInfo.GetDrives()[0].ToString());
            ManagerDialogWindows = dialog;
            ManagerLogger = logger;
            MainMenu = main;
            AddMenu = add;
            foreach (var i in DriveInfo.GetDrives())
                AddMenu.Content.Add(new DirectoryInfo(i.Name));
            AddMenu.Content.Add(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)));
            AddMenu.Content.Add(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)));
            AddMenu.Content.Add(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));
            AddMenu.Content.Add(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
            UpdateContent();
        }

        public void MoveIn(Menu menu)
        {
            try
            {
                if(menu.GetCurrentElement().ToDirectoryInfo().Exists)
                {
                    DirectoryInfo prevDir = CurrentDirectory;
                    try
                    {
                        CurrentDirectory = menu.GetCurrentElement().ToDirectoryInfo();
                        ContentState = State.Default;
                        UpdateContent();
                        MainMenu.NullPos();
                        ManagerLogger.Log("Move_In", CurrentDirectory.FullName);
                    }
                    catch (Exception e)
                    {
                        CurrentDirectory = prevDir;
                        throw e;
                    }
                }
                else
                {
                    if (FileOpeningPriorities().ContainsKey(menu.GetCurrentElement().Extension))
                        System.Diagnostics.Process.Start(FileOpeningPriorities()[menu.GetCurrentElement().Extension], menu.GetCurrentElement().FullName);
                    else
                        System.Diagnostics.Process.Start(menu.GetCurrentElement().FullName);
                    ManagerLogger.Log("Move_In", menu.GetCurrentElement().FullName);
                }
            }
            catch (Exception e)
            {
                ManagerDialogWindows.ErrorMessage(e.Message);
                ManagerLogger.Log("Move_In_Failure", $"{e.Message}; Path - {MainMenu.GetCurrentElement()}");
            }
        }

        public void MoveOut()
        {
            DirectoryInfo prevDir = CurrentDirectory;
            try
            {
                if (ContentState == State.Default)
                    CurrentDirectory = CurrentDirectory.Parent;
                ContentState = State.Default;
                UpdateContent();
                MainMenu.NullPos();
                ManagerLogger.Log("Move_Out", prevDir.FullName);
            }
            catch
            {
                ManagerDialogWindows.ErrorMessage("There is no way out");
                CurrentDirectory = prevDir;
            }
        }

        public void Search() => Search(ManagerDialogWindows.Input("Input search parameter:"));

        public void Search(string param)
        {
            if (param != String.Empty)
            {
                if (ContentState == State.Searched || ContentState == State.History)
                    MoveOut();
                MainMenu.Content = CurrentDirectory.Search(param);
                Sort(new DirFilesComparer());
                SearchRes = new List<FileSystemInfo>(MainMenu.Content);
                ManagerLogger.Log("Search", CurrentDirectory.FullName);
                if (MainMenu.Content.Count == 0)
                {
                    ManagerDialogWindows.ErrorMessage("No results :(");
                    UpdateContent();
                }
                else
                    ContentState = State.Searched;
                MainMenu.NullPos();
            }
        }

        public void LastSearchRes()
        {
            ContentState = State.Searched;
            MainMenu.Content = SearchRes;
            MainMenu.NullPos();
        }

        public void ClearLog()
        {
            ManagerLogger.ClearLog();
            ManagerDialogWindows.Message("Log Cleared");
        }

        public void History()
        {
            MainMenu.Content = ManagerLogger.History().Inverse();
            if (MainMenu.Content.Count == 0)
            {
                ManagerDialogWindows.ErrorMessage("History is empty");
                UpdateContent();
            }
            else
                ContentState = State.History;
            MainMenu.NullPos();
        }

        public void Rename()
        {
            if(MainMenu.Content.Count > 0)
                Rename(ManagerDialogWindows.Input("Input new title:"));
        }

        public void Rename(string newName)
        {
            try
            {
                if (MainMenu.Content.Count > 0 && newName != String.Empty)
                {
                    if (MainMenu.GetCurrentElement().ToDirectoryInfo().Exists)
                        newName = Path.Combine(MainMenu.GetCurrentElement().ToDirectoryInfo().Parent.FullName, newName);
                    else
                        newName = Path.Combine(MainMenu.GetCurrentElement().ToFileInfo().DirectoryName, newName);
                    if ((!Directory.Exists(newName)) && (!File.Exists(newName) || FileReplace(newName)))
                        MainMenu.GetCurrentElement().ToDirectoryInfo().MoveTo(newName);
                    UpdateContent();
                    ManagerLogger.Log("Rename", $"{MainMenu.GetCurrentElement()} -> {newName}");
                }
            }
            catch (Exception e)
            {
                ManagerDialogWindows.ErrorMessage(e.Message);
                ManagerLogger.Log("Rename_Failure", $"{e.Message}; Path - {MainMenu.GetCurrentElement()} -> {newName}");
            }
        }

        public void CreateDirectory() => CreateDirectory(ManagerDialogWindows.Input("Input new directory title"));

        public void CreateDirectory(string dir)
        {
            if (dir != String.Empty)
            {
                if (ContentState == State.Searched || ContentState == State.History)
                    MoveOut();
                dir = Path.Combine(CurrentDirectory.FullName, dir);
                if (Directory.Exists(dir))
                {
                    ManagerDialogWindows.ErrorMessage($"Directory called {dir} already exists");
                    ManagerLogger.Log("Create_Directory_Failure", $"Directory called {dir} already exists; Path - {dir}");
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(dir);
                        UpdateContent();
                        ManagerLogger.Log("Create_Directory", dir);
                    }
                    catch (Exception e)
                    {
                        ManagerDialogWindows.Message(e.Message);
                        ManagerLogger.Log("Create_Directory_Failure", $"{e.Message}; Path - {dir}");
                    }
                }
            }
        }

        public void Delete()
        {
            bool file = true;
            if (MainMenu.Content.Count > 0)
            {
                string name = MainMenu.GetCurrentElement().FullName;
                try
                {
                    if (ManagerDialogWindows.Confirmation($"Are you sure, you want delete {MainMenu.GetCurrentElement()}?"))
                    {
                        if (MainMenu.GetCurrentElement().ToDirectoryInfo().Exists)
                        {
                            file = false;
                            MainMenu.GetCurrentElement().ToDirectoryInfo().Delete(true);
                        }
                        else
                            MainMenu.GetCurrentElement().ToFileInfo().Delete();
                        MainMenu.ControllPos();
                        UpdateContent();
                        ManagerLogger.Log("Delete", name);
                    }
                }
                catch (Exception e)
                {
                    ManagerDialogWindows.ErrorMessage(e.Message);
                    ManagerLogger.Log($"{(file ? "File" : "Directory")}_Delete_Failure", $"{e.Message}; Path - {name}");
                }
            }
        }

        bool FileReplace(string file)
        {
            bool conf = ManagerDialogWindows.Confirmation($"File called {file} already exists. Are you sure, you want delete and replace it?");
            if (conf)
            {
                try { File.Delete(file); }
                catch (Exception e)
                {
                    ManagerDialogWindows.ErrorMessage(e.Message);
                    ManagerLogger.Log("File_Delete_Failure", $"{e.Message}; Path - {file}");
                }
            }
            return conf;
        }

        public void CreateFile() => CreateFile(ManagerDialogWindows.Input("Input new file title"));

        public void CreateFile(string file)
        {
            if (file != String.Empty)
            {
                if (ContentState == State.Searched || ContentState == State.History)
                    MoveOut();
                file = Path.Combine(CurrentDirectory.FullName, file);
                if (!File.Exists(file) || FileReplace(file))
                {
                    try
                    {
                        using (File.Create(file)) { }
                        UpdateContent();
                        ManagerLogger.Log("Create_File", file);
                    }
                    catch (Exception e)
                    {
                        ManagerDialogWindows.ErrorMessage(e.Message);
                        ManagerLogger.Log("File_Create_Failure", $"{e.Message}; Path - {file}");
                    }
                }
            }
        }

        public void CopyFrom() => CopyFrom(MainMenu.GetCurrentElement().FullName);

        public void MoveFrom() => MoveFrom(MainMenu.GetCurrentElement().FullName);

        public void CopyFrom(string path)
        {
            if (ContentState != State.History)
            {
                CopyPath = path;
                IsCopy = true;
            }
        }

        public void MoveFrom(string path)
        {
            if (ContentState != State.History)
            {
                CopyPath = path;
                IsCopy = false;
            }
        }

        public void Paste()
        {
            if (CopyPath == string.Empty)
                ManagerDialogWindows.ErrorMessage("Buffer is empty");
            else
            {
                if (ContentState == State.Searched || ContentState == State.History)
                    MoveOut();
                string newPath = Path.Combine(CurrentDirectory.FullName, Path.GetFileName(CopyPath));
                try
                {
                    if (!IsCopy && CurrentDirectory.Root.FullName == Directory.GetDirectoryRoot(CopyPath)
                        && ((!File.Exists(newPath) && !Directory.Exists(newPath))
                        || (File.Exists(CopyPath) && FileReplace(newPath))))
                        Directory.Move(CopyPath, newPath);
                    else
                    {
                        if (Directory.Exists(CopyPath)
                            && (!Directory.Exists(newPath)
                                || ManagerDialogWindows.Confirmation($"Directory called {Path.GetFileName(CopyPath)} already exists here. Do you want to concatanate folders?")))
                            new DirectoryInfo(CopyPath).CopyTo(CurrentDirectory.FullName);
                        else if (File.Exists(CopyPath) && (!File.Exists(newPath) || FileReplace(newPath)))
                            File.Copy(CopyPath, newPath);
                        if (!IsCopy)
                            if (Directory.Exists(CopyPath))
                                Directory.Delete(CopyPath, true);
                            else
                                File.Delete(CopyPath);
                    }
                    ManagerLogger.Log(IsCopy ? "Copy" : "Move", $"{CopyPath} -> {Directory.GetParent(newPath)}");
                    UpdateContent();
                }
                catch (Exception e)
                {
                    ManagerDialogWindows.ErrorMessage(e.Message);
                    ManagerLogger.Log(IsCopy ? "Copy_Failure" : "Move_Failure", $"{e.Message}; Path - {CopyPath} -> {newPath}");
                }
                CopyPath = String.Empty;
            }
        }

        public void SetPath() => SetPath(ManagerDialogWindows.Input("Input path"));

        public void SetPath(string newPath)
        {
            DirectoryInfo prevDir = CurrentDirectory;
            try
            {
                if (Path.IsPathRooted(newPath))
                {
                    CurrentDirectory = new DirectoryInfo(newPath);
                    UpdateContent();
                    ManagerLogger.Log("Set_Path", $"{prevDir.FullName} -> {newPath}");
                }
                else
                    throw new Exception("Incorrect path");
            }
            catch (Exception e)
            {
                ManagerDialogWindows.ErrorMessage(e.Message);
                ManagerLogger.Log("Set_Path_Failure", $"{e.Message}; Path - {newPath}");
                CurrentDirectory = prevDir;
            }
            ContentState = State.Default;
            MainMenu.NullPos();
        }

        public void Help(string helpFileName)
        {
            if (!File.Exists(helpFileName))
                ManagerDialogWindows.ErrorMessage($"File {helpFileName} doesn't exist");
            else
                if(FileOpeningPriorities().ContainsKey(Path.GetExtension(helpFileName)))
                    System.Diagnostics.Process.Start(FileOpeningPriorities()[Path.GetExtension(helpFileName)], helpFileName);
            else
                System.Diagnostics.Process.Start(helpFileName);
        }

        public void Sort(IComparer<FileSystemInfo> comp)
        {
            MainMenu.Content.Sort(comp);
        }

        public void MakeDescending()
        {
            MainMenu.Content = MainMenu.Content.Inverse();
        }

        public void UpdateContent()
        {
            MainMenu.Content = CurrentDirectory.GetDirectories().ToList<FileSystemInfo>();
            foreach (var file in CurrentDirectory.GetFiles())
                MainMenu.Content.Add(file.ToDirectoryInfo());
        }

        public void SelectedInfo()
        {
            if(MainMenu.Content.Count > 0)
                ManagerDialogWindows.SystemEntryInfoMsg(MainMenu.GetCurrentElement());
        }

        public void CurrentDirInfo() => ManagerDialogWindows.SystemEntryInfoMsg(CurrentDirectory);

        Dictionary<string, string> FileOpeningPriorities()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            try
            {
                using (StreamReader r = new StreamReader("FileOpeningConfig.txt"))
                {
                    while (!r.EndOfStream)
                    {
                        string str = r.ReadLine();
                        string[] t = str.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                        res.Add(t[0], t[1]);
                    }
                }
        }
            catch (ArgumentException) { }
            return res;
        }
    }

    public enum State { Default, Searched, History }

}