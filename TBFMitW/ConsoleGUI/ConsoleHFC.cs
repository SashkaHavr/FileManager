using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace ConsoleGUI
{
    class ConsoleHFC : HelpfulFilesCreator
    {
        public new void CreateREADME()
        {
            if (!File.Exists("README.txt"))
                using (StreamWriter w = new StreamWriter("README.txt"))
                    w.Write($"{base.GetReadmeText()}{Environment.NewLine}All control keys are shown in Help.txt");
        }

        public void CreateHelp()
        {
            if (!File.Exists("Help.txt"))
                using (StreamWriter w = new StreamWriter("Help.txt"))
                    w.WriteLine($"Enter - Move in | BackSpace - Move Out{Environment.NewLine}" +
                                    $"Tab - Jump Between Additional and Main Field{Environment.NewLine}" +
                                    $"S - Search{Environment.NewLine}" +
                                    $"F9 - Load Last Searching Result{Environment.NewLine}" +
                                    $"H - History | F10 - Clear History{Environment.NewLine}" +
                                    $"D - Create Directory{Environment.NewLine}" +
                                    $"F - Create File{Environment.NewLine}" +
                                    $"Del - Delete{Environment.NewLine}" +
                                    $"R - Rename{Environment.NewLine}" +
                                    $"C - Copy | X - Cut{Environment.NewLine}" +
                                    $"V - Paste{Environment.NewLine}" +
                                    $"P - Set Path{Environment.NewLine}" +
                                    $"F2 - Get File/Directory Information{Environment.NewLine}" +
                                    $"F1 - Help{Environment.NewLine}" +
                                    $"F4 - Change from default to descending sort{Environment.NewLine}" +
                                    $"F5 - Sort by name{Environment.NewLine}" +
                                    $"F6 - Sort by directories/files{Environment.NewLine}" +
                                    $"F7 - Sort by last write date{Environment.NewLine}" +
                                    $"F8 - Sort by file size");
        }
    }
}
