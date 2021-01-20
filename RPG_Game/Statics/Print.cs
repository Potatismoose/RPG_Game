﻿using RPG_Game.Enemies;
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
        public static void ClearAllScreen()
        {
            Console.SetCursorPosition(0, 10);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));

            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(0, 10);
        }
        
        public static void ClearAllScreen(int left, int top)
        {
            Console.SetCursorPosition(0, 10);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));

            }
            Console.SetCursorPosition(0, 0);
            Console.SetCursorPosition(left, top);
        }
        public static void FightConsolePrintText(List<string> fightText, Player player, Enemy enemy) 
        {
            int consolePrintPositionTop = 33;
            int consolePrintPositionLeft = 0;
            foreach (var item in fightText)
            {
                Console.SetCursorPosition(consolePrintPositionLeft, consolePrintPositionTop);
                Print.Yellow(item);
                consolePrintPositionTop++;
            }
            int hpStatsCursorTop = 34;
            int hpStatsCursorLeft = 93;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                if (i == 0)
                {
                    if (player.Alive)
                    {
                        Console.Write(new string(' ', 14));
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"Player HP: {player.Health}");
                    }
                    else
                    {
                        Console.Write(new string(' ', 14));
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"You died!");
                    }
                    
                }

                if (i == 2)
                {
                    if (enemy.Alive)
                    {
                        Console.Write(new string(' ', 14));
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"Enemy HP: {enemy.Health}");
                    }
                    else
                    {
                        Console.Write(new string(' ', 14));
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"Enemy is dead!");
                    }
                }
                hpStatsCursorTop++;

            }
        }
        public static void FightConsole()
        {
            int topConsoleBorder = 32;
            int leftConsoleBorder = 0;
            int rows = 8;
            int width = 110;
            int placement = 91;
            Console.SetCursorPosition(leftConsoleBorder, topConsoleBorder);

            for (int i = 0; i < rows; i++)
            {
                if (i == 0 || i == rows-1)
                {
                    Console.SetCursorPosition(leftConsoleBorder, topConsoleBorder + i);
                    Red(new string('*', width));
                }
            }
            Console.SetCursorPosition(leftConsoleBorder+placement, topConsoleBorder);
            for (int i = 0; i < rows-2; i++)
            {

                Console.SetCursorPosition(leftConsoleBorder + placement, topConsoleBorder+i+1);
                Red("*                 *");
                
            }
        }
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
        public static void PotionPrint()
        {
            switch (1)
            {
                case 1:
                    Blue("   |~|");
                    Blue("   | |  ");
                    Blue(" .'   `.");
                    Blue(" `.___.'");

                    break;
                //default:
                //    break;
            }
        }
        public static void PlayerStatsPrint(Player player)
        {
            int deleteColumn = 105;
            int deleteRow = 1;
            //For loop som skriver över statsrutan för att uppdatera playerstats
            for (int i = 0; i < 9; i++)
            {
                Console.SetCursorPosition(deleteColumn, deleteRow);
                Console.Write(new string(' ', 30));
                deleteRow++;
            }
            //skriver ut player stats igen
            player.PrintCurrentPlayerStatus();
            Console.ReadKey();
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
        public static void SetTopLeftCursorPosToStandard()
        {
            top = 13;
            left = 45;
            Console.SetCursorPosition(left, top);
        }
        public static void SetTopLeftCursorPosToStandard(int leftModify, int topModify,char type)
        {
            top = 13;
            left = 45;
            if (type == '-')
            {
                Console.SetCursorPosition(left-leftModify, top-topModify);
            }
            else if (type == '+')
            {
                Console.SetCursorPosition(left + leftModify, top + topModify);
            }
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
            Print.FightConsole();
            for (int i = 0; i < 1; i++)
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
                Print.ClearAllScreen();
                
                
                player.PrintCurrentPlayerStatus();
                Print.FightConsole();
                SetTopLeftCursorPosToStandardDragon();
                for (int j = 0; j < dragon.Count; j++)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Blue(dragon[j]);
                    top++;
                }
                
                WeaponAnimation(true);
                
            }
        }


    }
}
