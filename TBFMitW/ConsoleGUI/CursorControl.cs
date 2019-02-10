using System;

namespace ConsoleGUI
{
    static class CursorControl
    {
        public static void MainWindowPosition(int y = 0) => Console.SetCursorPosition(22, 5 + y);
        public static void AddWindowPosition(int y = 0) => Console.SetCursorPosition(2, 1 + y);
        public static void DateMainWindowPosition(int y = 0) => Console.SetCursorPosition(Console.WindowWidth - 37, 5   + y);
        public static void SizeMainWindowPosition(int y = 0) => Console.SetCursorPosition(Console.WindowWidth - 15, 5 + y);
        public static void DialogWindowPosition(int y = 0) => Console.SetCursorPosition(Console.WindowWidth / 4 + Console.WindowWidth / 20, Console.WindowHeight / 4 + 2 + y);
        public static void DialogWindowEndPosition(int y = 0) => Console.SetCursorPosition(Console.WindowWidth / 4 + Console.WindowWidth / 20 + (Console.WindowWidth / 2 - Console.WindowWidth / 10), Console.WindowHeight / 4 + 2 + y);
        public static void LoadingPosition() => Console.SetCursorPosition(Console.WindowWidth / 40 * 17 + 1, Console.WindowHeight / 40 * 19 + 1);
    }
}
