using System;
using System.IO;

namespace FileManager
{
    public class HelpfulFilesCreator
    {

        public void CreateFileOpeningConfig()
        {
            if (!File.Exists("FileOpeningConfig.txt"))
                using (File.Create("FileOpeningConfig.txt")) { }
        }
        public void CreateREADME()
        {
            if (!File.Exists("README.txt"))
                using (StreamWriter w = new StreamWriter("README.txt"))
                    w.WriteLine(GetReadmeText());
        }

        public void AddFileOpeningConfigKey(string extentionKey, string programPath)
        {
            if (File.Exists("FileOpeningConfig.txt"))
                using (StreamWriter w = new StreamWriter("FileOpeningConfig.txt", true))
                    w.Write($"{extentionKey}|{programPath}");
        }

        protected string GetReadmeText() => $"You can set standart programs for file extentions:{Environment.NewLine}" +
                        $"File Opening Config Format - <.Extention>|<Program Path>{Environment.NewLine}" +
                        $"Example - .txt|C:\\Program Files\\Notepad++\\notepad++.exe";
    }
}
