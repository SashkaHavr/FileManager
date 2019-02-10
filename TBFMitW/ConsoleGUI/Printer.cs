using System;
using System.IO;
using FileManager;
using System.Collections.Generic;

namespace ConsoleGUI
{
    class Printer
    {
        ManagerControl mc;
        BackGroundPrinter backGroundPrinter = new BackGroundPrinter();
        List<string> displayableNames;
        List<string> displayableDates;
        List<string> displayableSizes;

        public Printer(ManagerControl m) { mc = m; }

        public void Display()
        {
            if (!mc.NoNeedScreenUpdate || (mc.manager.MainMenu as PageMenu).Scrolled)
            {
                displayableDates = new List<string>();
                displayableNames = new List<string>();
                displayableSizes = new List<string>();
                List<FileSystemInfo> displayableContent;
                if (mc.manager.MainMenu.Content.Count > ConsoleHelpers.GetMaxStrings())
                    displayableContent = mc.manager.MainMenu.Content.LeaveRange((mc.manager.MainMenu as PageMenu).Page * ConsoleHelpers.GetMaxStrings(), ((mc.manager.MainMenu as PageMenu).Page + 1) * ConsoleHelpers.GetMaxStrings());
                else
                    displayableContent = mc.manager.MainMenu.Content;
                foreach (var fsi in displayableContent)
                {
                    displayableNames.Add(fsi.Name);
                    displayableDates.Add(fsi.LastWriteTime.ToString());
                    displayableSizes.Add(fsi.ToFileInfo().GetSize());
                }
                backGroundPrinter.BackGround();
                if (mc.manager.MainMenu.Content.Count > 0)
                {
                    PrintMain();
                    PrintProperties();
                }
                PrintAdd();
                PrintPagesCount();
                PrintPath();
                (mc.manager.MainMenu as PageMenu).Scrolled = false;
            }
            else
            {
                if (mc.manager.MainMenu.Content.Count > 0)
                {
                    PrintMainMovement();
                    PrintPropertiesMovement();
                }
                PrintAddMovement();
            }
            PrintAttr();
            mc.NoNeedScreenUpdate = false;
        }
        void PrintMain()
        {
            for (int i = 0; i <displayableNames.Count; i++)
            {
                CursorControl.MainWindowPosition(i);
                if (mc.manager.MainMenu.CurPosition == i && mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintName(displayableNames[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    PrintName(displayableNames[i]);
            }
        }
        void PrintAdd()
        {
            for (int i = 0; i < mc.manager.AddMenu.Content.Count; i++)
            {
                CursorControl.AddWindowPosition(i);
                if (mc.manager.AddMenu.CurPosition == i && !mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintName(mc.manager.AddMenu.Content[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    PrintName(mc.manager.AddMenu.Content[i].Name);
            }
        }
        void PrintProperties()
        {
            for (int i = 0; i < displayableNames.Count; i++)
            {
                CursorControl.DateMainWindowPosition(i);
                if (mc.manager.MainMenu.CurPosition == i && mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    CursorControl.DateMainWindowPosition(i);
                    Console.WriteLine(displayableDates[i]);
                    CursorControl.SizeMainWindowPosition(i);
                    Console.WriteLine(displayableSizes[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    CursorControl.DateMainWindowPosition(i);
                    Console.WriteLine(displayableDates[i]);
                    CursorControl.SizeMainWindowPosition(i);
                    Console.WriteLine(displayableSizes[i]);
                }

            }
        }
        void PrintPagesCount()
        {
            CursorControl.AddWindowPosition(Console.WindowHeight - 3);
            if (mc.manager.MainMenu.Content.Count % ConsoleHelpers.GetMaxStrings() == 0)
                Console.Write($"Page - {(mc.manager.MainMenu as PageMenu).Page + 1}/" +
    $"{ mc.manager.MainMenu.Content.Count / ConsoleHelpers.GetMaxStrings()}");
            else
            Console.Write($"Page - {(mc.manager.MainMenu as PageMenu).Page + 1}/" +
                $"{ mc.manager.MainMenu.Content.Count / ConsoleHelpers.GetMaxStrings() + 1}");
        }
        void PrintMainMovement()
        {
            if (mc.manager.MainMenu.CurPosition == displayableNames.Count - 1)
            {
                CursorControl.MainWindowPosition(0);
                PrintName(displayableNames[0]);
            }
            else if (mc.manager.MainMenu.CurPosition == 0)
            {
                CursorControl.MainWindowPosition(displayableNames.Count - 1);
                PrintName(displayableNames[displayableNames.Count - 1]);
            }
            for (int i = mc.manager.MainMenu.CurPosition !=0 ? mc.manager.MainMenu.CurPosition - 1 : 0;
                i < (mc.manager.MainMenu.CurPosition != (displayableNames.Count - 1) ? mc.manager.MainMenu.CurPosition + 2 :displayableNames.Count);
                i++)
            {
                CursorControl.MainWindowPosition(i);
                if (mc.manager.MainMenu.CurPosition == i && mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintName( displayableNames[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    PrintName(displayableNames[i]);
            }

        }
        void PrintAddMovement()
        {
            if (mc.manager.AddMenu.CurPosition == mc.manager.AddMenu.Content.Count - 1)
            {
                CursorControl.AddWindowPosition(0);
                PrintName(mc.manager.AddMenu.Content[0].Name);
            }
            else if (mc.manager.AddMenu.CurPosition == 0)
            {
                CursorControl.AddWindowPosition(mc.manager.AddMenu.Content.Count - 1);
                PrintName(mc.manager.AddMenu.Content[mc.manager.AddMenu.Content.Count - 1].Name);
            }
            for (int i = mc.manager.AddMenu.CurPosition != 0 ? mc.manager.AddMenu.CurPosition - 1 : 0;
                i < (mc.manager.AddMenu.CurPosition != (mc.manager.AddMenu.Content.Count - 1) ? mc.manager.AddMenu.CurPosition + 2 : mc.manager.AddMenu.Content.Count);
                i++)
            {
                CursorControl.AddWindowPosition(i);
                if (mc.manager.AddMenu.CurPosition == i && !mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintName(mc.manager.AddMenu.Content[i].Name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    PrintName(mc.manager.AddMenu.Content[i].Name);
            }

        }
        void PrintPropertiesMovement()
        {
            if (mc.manager.MainMenu.CurPosition == displayableNames.Count - 1)
            {
                CursorControl.DateMainWindowPosition(0);
                Console.WriteLine(displayableDates[0]);
                CursorControl.SizeMainWindowPosition(0);
                Console.WriteLine(displayableSizes[0]);
            }
            else if(mc.manager.MainMenu.CurPosition == 0)
            {
                CursorControl.DateMainWindowPosition(displayableNames.Count - 1);
                Console.WriteLine(displayableDates[displayableNames.Count - 1]);
                CursorControl.SizeMainWindowPosition(displayableNames.Count - 1);
                Console.WriteLine(displayableSizes[displayableNames.Count - 1]);
            }
            for (int i = mc.manager.MainMenu.CurPosition != 0 ? mc.manager.MainMenu.CurPosition - 1 : 0;
                i < (mc.manager.MainMenu.CurPosition != (displayableNames.Count - 1) ? mc.manager.MainMenu.CurPosition + 2 : displayableNames.Count);
                i++)
            {
                if (mc.manager.MainMenu.CurPosition == i && mc.MainActive)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    CursorControl.DateMainWindowPosition(i);
                    Console.WriteLine(displayableDates[i]);
                    CursorControl.SizeMainWindowPosition(i);
                    Console.WriteLine(displayableSizes[i]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    CursorControl.DateMainWindowPosition(i);
                    Console.WriteLine(displayableDates[i]);
                    CursorControl.SizeMainWindowPosition(i);
                    Console.WriteLine(displayableSizes[i]);
                }

            }
        }
        void PrintName(string filePath)
        {
            if (filePath.Length > 3)
            {
                if (filePath.Length > Console.WindowWidth - 60)
                    Console.WriteLine(filePath.Remove(Console.WindowWidth - 66) + ".." + Path.GetExtension(filePath));
                else
                    Console.WriteLine(filePath);
            }
            else
                Console.WriteLine(filePath);
        }
        void PrintPath()
        {
            Console.SetCursorPosition(22, 1);
            Console.ForegroundColor = ConsoleColor.Green;
            switch(mc.manager.ContentState)
            {
                case State.Searched:
                    Console.WriteLine("Search Results");
                    break;
                case State.History:
                    Console.WriteLine("History");
                    break;
                case State.Default:
                    if (mc.manager.CurrentDirectory.FullName.Length < Console.WindowWidth - 60)
                        Console.Write(mc.manager.CurrentDirectory.FullName);
                    else
                        Console.Write(mc.manager.CurrentDirectory.Parent.FullName.Remove(Console.WindowWidth - mc.manager.CurrentDirectory.Name.Length - 63)
                            + "..." + mc.manager.CurrentDirectory.Name);
                    break;
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        void PrintAttr()
        {
            if (mc.manager.CopyPath != String.Empty)
            {
                Console.SetCursorPosition(Console.WindowWidth - 37, 1);
                for (int i = 0; i < 35; i++)
                    Console.Write(' ');
                Console.SetCursorPosition(Console.WindowWidth - 37, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                if(Path.GetFileName(mc.manager.CopyPath).Length < 17)
                    Console.WriteLine($"{(mc.manager.IsCopy ? "Copy" : "Move")} Buffer - {Path.GetFileName(mc.manager.CopyPath)}");
                else
                    Console.WriteLine($"{(mc.manager.IsCopy ? "Copy" : "Move")} Buffer - {Path.GetFileName(mc.manager.CopyPath).Remove(14)}..{Path.GetExtension(mc.manager.CopyPath)} ");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
