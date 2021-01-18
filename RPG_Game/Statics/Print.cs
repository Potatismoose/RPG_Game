using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPG_Game
{
    static class Print
    {

        private static int top;
        private static int left;
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

        private static void SetTopLeftCursorPosToStandardSword()
        {
            top = 13;
            left = 25;
        }
        private static void SetTopLeftCursorPosToStandardDragon()
        {
            top = 11;
            left = 0;
        }

        public static void WeaponAnimation(bool dragonTurn)
        
        {
            
            List<String> sword = new List<String>();
            sword.Add("               ¤¤");
            sword.Add("              ¤¤¤¤");
            sword.Add("              |§ |");
            sword.Add("              | §|");
            sword.Add("              |§ |");
            sword.Add("              | §|");
            sword.Add("              |§ |");
            sword.Add("              | §|");
            sword.Add("              |§ |");
            sword.Add("              | §|");
            sword.Add("              |§ |");
            sword.Add("              | §|      ");
            sword.Add("              |§ |      ");
            sword.Add(@$"          /¯¥¯¯¯¯¯¯¥¯\      ");
            sword.Add(@$"          ¯¯¯¯|¯¯|¯¯¯¯      ");
            sword.Add("              |¥¥|      ");
            sword.Add("              |¥¥|      ");
            sword.Add(@$"             /¯¯¯¯\      ");
            sword.Add("             ¯¯¯¯¯¯      ");

            List<String> swordSwing = new List<String>();
            swordSwing.Add(@"¤ ¤               ");
            swordSwing.Add(@"¤ §\              ");
            swordSwing.Add(@" \§ \             ");
            swordSwing.Add(@"  \ §\            ");
            swordSwing.Add(@"   \§ \           ");
            swordSwing.Add(@"    \ §\          ");
            swordSwing.Add(@"     \§ \         ");
            swordSwing.Add(@"      \ §\        ");
            swordSwing.Add(@"       \§ \       ");
            swordSwing.Add(@"        \ §\      ");
            swordSwing.Add(@"         \§ \     ");
            swordSwing.Add(@"          \ §\   /¯/    ");
            swordSwing.Add(@"           \§ \ ¥ ¥    ");
            swordSwing.Add(@"            \ §\ /     ");
            swordSwing.Add(@"             \§ /     ");
            swordSwing.Add(@"             / / \     ");
            swordSwing.Add(@"            ¥ ¥\¥¥\     ");
            swordSwing.Add(@"           / /  \¥¥\     ");
            swordSwing.Add(@"           ¯¯    \ /     ");


            if (dragonTurn)
            {
                SetTopLeftCursorPosToStandardSword();
                for (int j = 0; j < sword.Count; j++)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Blue(sword[j]);
                    top++;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    SetTopLeftCursorPosToStandardSword();
                    for (int j = 0; j < sword.Count; j++)
                    {
                        Console.SetCursorPosition(left, top);
                        Print.Blue(sword[j]);
                        top++;
                    }

                    Thread.Sleep(300);
                    SetTopLeftCursorPosToStandardSword();
                    for (int j = 0; j < swordSwing.Count; j++)
                    {
                        Console.SetCursorPosition(left, top);
                        Print.Blue(swordSwing[j]);
                        top++;
                    }
                    AudioPlaybackEngine.Instance.PlaySound("swordhit.mp3");
                    Thread.Sleep(300);
                    SetTopLeftCursorPosToStandardSword();
                    for (int j = 0; j < sword.Count; j++)
                    {
                        Console.SetCursorPosition(left, top);
                        Print.Blue(sword[j]);
                        top++;
                    }
                }
            }


        }

        public static void DragonAnimation(Player player)
        {

            List<String> dragon = new List<String>();
            {
                dragon.Add("<~>");
                dragon.Add(@" \ \,_____");
                dragon.Add(@"       ___`\");
                dragon.Add(@"       \('>\`-__");
                dragon.Add("         ~      ~~~--__");
                dragon.Add(@"               ______(@\");
                dragon.Add(@"              /******~~~~\");
                dragon.Add(@"      \       `--____");
                dragon.Add("     / ~~~--_____    ~~~/");
                dragon.Add("    /            `~~~~~  ");
                dragon.Add("   /            ");
                dragon.Add("  /           ");
            }

            List<String> dragonFire = new List<String>();
            {
                dragonFire.Add("<~>");
                dragonFire.Add(@" \ \,_____");
                dragonFire.Add(@"       ___`\");
                dragonFire.Add(@"       \('>\`-__");
                dragonFire.Add("         ~      ~~~--__*****");
                dragonFire.Add(@"               ______(@\   *******  ****    *******    ******");
                dragonFire.Add(@"              /******~~~~\**********************************");
                dragonFire.Add(@"      \       `--____******************************************");
                dragonFire.Add("     / ~~~--_____    ~~~/  ***************************************");
                dragonFire.Add("    /            `~~~~~         ******************************");
                dragonFire.Add("   /                                 ****    **************");
                dragonFire.Add("  /                                    ***       ***********  ");
            }

            
            WeaponAnimation(true);
            
            for (int i = 0; i < 2; i++)
            {
                SetTopLeftCursorPosToStandardDragon();
                for (int j = 0; j < dragon.Count; j++)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Blue(dragon[j]);
                    top++;
                }
                
                Thread.Sleep(800);
                SetTopLeftCursorPosToStandardDragon();
                for (int j = 0; j < dragonFire.Count; j++)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Red(dragonFire[j]);
                    top++;
                }
                Thread.Sleep(500);
                Console.Clear();
                LogoPrint();
                player.PrintCurrentPlayerStatus();

                SetTopLeftCursorPosToStandardDragon();
                for (int j = 0; j < dragon.Count; j++)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Blue(dragon[j]);
                    top++;
                }
                //Remove this
                WeaponAnimation(true);
                //WeaponAnimation(true);
            }
        }


    }
}
