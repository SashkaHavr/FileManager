using System;
using FileManager;

namespace ConsoleGUI
{
    class Program
    {
        static void Main()
        {
            var hfc = new ConsoleHFC();
            hfc.CreateFileOpeningConfig();
            hfc.CreateREADME();
            hfc.CreateHelp();
            Manager manager = new Manager(new DialogWindows(), new XmlLogger(), new PageMenu(), new Menu());
            ManagerControl managerControl = new ManagerControl(manager);
            Printer printer = new Printer(managerControl);
            do
            {
                printer.Display();
                ConsoleHelpers.Key = Console.ReadKey(true);
                managerControl.MenuControl();
            } while (ConsoleHelpers.Key.Key != ConsoleKey.Escape);
        }
    }
}