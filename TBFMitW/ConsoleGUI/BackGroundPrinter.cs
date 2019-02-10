using System;
using System.Text;

namespace ConsoleGUI
{
    class BackGroundPrinter
    {
        public BackGroundPrinter()
        {
            Console.OutputEncoding = Console.OutputEncoding = Encoding.UTF8;
            System.Windows.Forms.SendKeys.SendWait("{F11}");
            Console.Title = "The best file manager in the world";
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
        public void BackGround()
        {
            Console.Clear();
            Borders();
            Header();
            HorizontalLine(4, 20, Console.WindowWidth);
            HorizontalLine(2, 20, Console.WindowWidth);
            VerticalLine(21, 0, Console.WindowHeight);
            VerticalLine(Console.WindowWidth - 38, 2, Console.WindowHeight);
        }

        public void Borders()
        {
            VerticalLine(1, 0, Console.WindowHeight);
            VerticalLine(Console.WindowWidth - 2, 0, Console.WindowHeight);
            HorizontalLine(0, 0, Console.WindowWidth);
            HorizontalLine(Console.WindowHeight - 1, 0, Console.WindowWidth);
            Console.SetCursorPosition(Console.WindowWidth - 2, Console.WindowHeight - 1);
            Console.Write("╝");
            Console.SetCursorPosition(1, 0);
            Console.Write('╔');
            Console.SetCursorPosition(Console.WindowWidth - 2, 0);
            Console.Write('╗');
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.Write('╚');
        }

        public void VerticalLine(int x, int y, int endy)
        {
            for (int i = y+1; i < endy - 1; i++)
            {
                Console.SetCursorPosition(x, i);
                Console.Write('║');
            }
            Console.SetCursorPosition(x, y);
            Console.Write('╦');
            Console.SetCursorPosition(x, endy - 1);
            Console.Write('╩');
        }
        public void HorizontalLine(int y, int x, int endx)
        {
            for (int i = x + 2; i < endx - 2; i++)
            {
                Console.SetCursorPosition(i, y);
                Console.Write('═');
            }
            Console.SetCursorPosition(x + 1, y);
            Console.Write('╠');
            Console.SetCursorPosition(endx - 2, y);
            Console.Write('╣');
        }
        void Header()
        {
            Console.SetCursorPosition(22, 3);
            Console.WriteLine("Name");
            Console.SetCursorPosition(Console.WindowWidth - 37, 3);
            Console.WriteLine("Last Change Date");
            Console.SetCursorPosition(Console.WindowWidth - 12, 3);
            Console.WriteLine("Size");
            HorizontalLine(Console.WindowHeight - 3, 0, 23);
        }
    }

}
