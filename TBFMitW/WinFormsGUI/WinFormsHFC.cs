using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManager;

namespace WinFormsGUI
{
    class WinFormsHFC : HelpfulFilesCreator
    {
        public new void CreateREADME()
        {
            if (!File.Exists("README.txt"))
                using (StreamWriter w = new StreamWriter("README.txt"))
                    w.Write($"{base.GetReadmeText()}{Environment.NewLine}You can also adit it in manager");
        }

    }
}
