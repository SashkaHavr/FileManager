using System;

namespace ConsoleGUI
{
    static class ConsoleHelpers
    {
        public static ConsoleKeyInfo Key { get; set; } = new ConsoleKeyInfo();
        public static int GetMaxStrings() => Console.WindowHeight - 6;
    }
}
