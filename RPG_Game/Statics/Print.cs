using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game
{
    static class Print
    {
        /*
---------------------------------------------------------------
METHODS FOR FORMATING AND PRINTING TEXT IN DIFFERENT COLORS
---------------------------------------------------------------
*/
        public static void Red(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void RedW(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Yellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void YellowW(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Green(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void Grey(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ResetColor();

        }
        public static void Blue(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            if (text.Contains("alternativ"))
            {
                Console.Write(text);
            }
            else
            {
                Console.WriteLine(text);
            }
            Console.ResetColor();

        }



        /*
        ---------------------------------------------------------------
                 METHODS FOR PRINTING GAME NAME AND GRAPHICS
        ---------------------------------------------------------------
        */

        public static void LogoPrint()
        {
            Console.WriteLine(Environment.NewLine);
            Print.Red("  ██████╗ ██████╗  █████╗  ██████╗  ██████╗ ███╗   ██╗     ██████╗ ██╗   ██╗███████╗███████╗████████╗");
            Print.Red("  ██╔══██╗██╔══██╗██╔══██╗██╔════╝ ██╔═══██╗████╗  ██║    ██╔═══██╗██║   ██║██╔════╝██╔════╝╚══██╔══╝");
            Print.Red("  ██║  ██║██████╔╝███████║██║  ███╗██║   ██║██╔██╗ ██║    ██║   ██║██║   ██║█████╗  ███████╗   ██║   ");
            Print.Red("  ██║  ██║██╔══██╗██╔══██║██║   ██║██║   ██║██║╚██╗██║    ██║▄▄ ██║██║   ██║██╔══╝  ╚════██║   ██║ ");
            Print.Red("  ██████╔╝██║  ██║██║  ██║╚██████╔╝╚██████╔╝██║ ╚████║    ╚██████╔╝╚██████╔╝███████╗███████║   ██║   ");
            var bottom = "  ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝     ╚══▀▀═╝  ╚═════╝ ╚══════╝╚══════╝   ╚═╝ ";
            Print.Red(bottom);
            for (int i = 0; i < bottom.Length; i++)
            {
                if (i == 0 || i == 1)
                {
                    Console.Write(" ");
                }
                else
                {
                    Print.RedW("*");
                }
            }
            Console.WriteLine(Environment.NewLine);

        }

        public static void DragonPrint()
        {
            Print.Blue("                 /            /");
            Print.Blue("                / ' .,,,,  ./       ");
            Print.Blue("               / ';'     ,/");
            Print.Blue("              / /    ,,//,`'`    ");
            Print.Blue("             ( ,, '_,  ,,,' ``     ");
            Print.Blue(@$"             |    /@  ,,, ; ; `         ");
            Print.Blue("             /    .   ,'' / ' `,``    ");
            Print.Blue("            /   .     ./, `,, ` ;     ");
            Print.Blue(@$"        ,./  .   ,-,',` ,,/''\,'");
            Print.Blue("       |   /; ./,,'`,,'' |   | ");
            Print.Blue("       |     / ','      /    |");
            Print.Blue(@$"        \___ / '   '   |     |");
            Print.Blue(@$"          `,,'    |    /     `\ ");
            Print.Blue(@$"                 /     |       ~\         ");
            Print.Blue("                '      (   ");
            Print.Blue("              :                   ");
            Print.Blue(@$"             ; .         \--");
            Print.Blue(@$"             ; .         \--");
            Print.Blue(@$"             \         ;");


        }


    }
}
