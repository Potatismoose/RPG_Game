using RPG_Game.Enemies;
using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
        public static void GreenW(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ResetColor();
        }
        public static void Grey(string text)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(text);
            Console.ResetColor();

        }
        public static void DarkGrey(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
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

        //Clearing the screen from information so no fragments are left when updating
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
        //mainly using this when clearing.
        public static void ClearAllScreen(int leftover, int topover)
        {

            //Delete inventory printout during fights
            if (topover == 18)
            {
                for (int i = 0; i < 22; i++)
                {
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(leftover, topover);
                    Console.Write(new string(' ', 28));
                    topover++;


                }
                topover = 18;
                Console.SetCursorPosition(leftover, topover);
            }
            //Delete inventory top header/status bar.
            else if (topover == 11 && leftover == 29)
            {
                for (int i = 0; i < 1; i++)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(leftover, topover);
                    Console.Write(new string(' ', 68));
                    topover++;


                }
                topover = 18;
                Console.SetCursorPosition(leftover, topover);
            }
            //Inventory right window
            else if (leftover == 28 && topover == 13)
            {
                for (int i = 0; i < 23; i++)
                {
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(leftover, topover);
                    Console.Write(new string(' ', 69));
                    topover++;
                }
            }
            else if (leftover == 104)
            {

                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(leftover, topover);
                    Console.Write(new string(' ', 35));
                    topover++;
                }
            }

            else if (leftover == 28 && topover != 13)
            {
                Console.CursorVisible = true;
                for (int i = 0; i < 28; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        Console.SetCursorPosition(leftover, topover);
                        Console.Write(new string(' ', 73));
                    }


                    topover++;
                }
            }


            else
            {
                for (int i = 0; i < Console.WindowHeight; i++)
                {
                    Console.SetCursorPosition(leftover, topover);
                    Console.Write(new string(' ', Console.WindowWidth));
                    topover++;
                }

            }

        }
        //Print text during fights
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
                        if (player.Health < player.MaxHealth / 5)
                        {
                            Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop - 1);
                            Print.Red("!!! WARNING !!!");
                            Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                            Console.Write("Player HP: ");
                            Print.Red($"{player.Health}");
                        }
                        else
                        {
                            Console.WriteLine($"Player HP: {player.Health}");
                        }
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
                        Print.Yellow($"{enemy.Type} ");
                        hpStatsCursorTop++;
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"Enemy HP: {enemy.Health} ");
                        hpStatsCursorTop++;
                        Console.SetCursorPosition(hpStatsCursorLeft, hpStatsCursorTop);
                        Console.WriteLine($"Strength: {enemy.Strength} ");
                        hpStatsCursorTop++;




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
        //Print the borders of the console during the fight
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
                if (i == 0 || i == rows - 1)
                {
                    Console.SetCursorPosition(leftConsoleBorder, topConsoleBorder + i);
                    Red(new string('*', width));
                }
            }
            Console.SetCursorPosition(leftConsoleBorder + placement, topConsoleBorder);
            for (int i = 0; i < rows - 2; i++)
            {

                Console.SetCursorPosition(leftConsoleBorder + placement, topConsoleBorder + i + 1);
                Red("*                 *");

            }
        }
        //Print the text logo
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
        //Print playerstats border during fights
        public static void PlayerStatsPrint(Player player)
        {
            int deleteColumn = 105;
            int deleteRow = 0;
            //For loop som skriver över statsrutan för att uppdatera playerstats
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(deleteColumn, deleteRow);
                Console.Write(new string(' ', 30));
                deleteRow++;
            }
            //skriver ut player stats igen
            player.PrintCurrentPlayerStatus();

        }
        //Print out the dragon in the main menu
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
        //For moving the cursor
        public static void SetTopLeftCursorPosToStandard()
        {
            top = 13;
            left = 45;
            Console.SetCursorPosition(left, top);
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
        //Weapon animation during endfight
        public static void WeaponAnimation(bool dragonTurn, Menu _menuObject)

        {

            List<String> sword = new List<String>
            {
                "               ¤¤",
                "              ¤¤¤¤",
                "              |§ |",
                "              | §|",
                "              |§ |",
                "              | §|",
                "              |§ |",
                "              | §|",
                "              |§ |",
                "              | §|",
                "              |§ |",
                "              | §|      ",
                "              |§ |      ",
                @$"          /¯¥¯¯¯¯¯¯¥¯\      ",
                @$"          ¯¯¯¯|¯¯|¯¯¯¯      ",
                "              |¥¥|      ",
                "              |¥¥|      ",
                @$"             /¯¯¯¯\      ",
                "             ¯¯¯¯¯¯      "
            };

            List<String> swordSwing = new List<String>
            {
                @"¤ ¤               ",
                @"¤ §\              ",
                @" \§ \             ",
                @"  \ §\            ",
                @"   \§ \           ",
                @"    \ §\          ",
                @"     \§ \         ",
                @"      \ §\        ",
                @"       \§ \       ",
                @"        \ §\      ",
                @"         \§ \     ",
                @"          \ §\   /¯/    ",
                @"           \§ \ ¥ ¥    ",
                @"            \ §\ /     ",
                @"             \§ /     ",
                @"             / / \     ",
                @"            ¥ ¥\¥¥\     ",
                @"           / /  \¥¥\     ",
                @"           ¯¯    \ /     "
            };



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
                    //LJUD HÄR
                    var sounds = _menuObject.SoundList();
                    AudioPlaybackEngine sound = new AudioPlaybackEngine();
                    sound.PlaySound(sounds[10]);
                    Thread.Sleep(300);
                    sound.Dispose();
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
        //Dragon animation endfight
        public static void DragonAnimation(Player player, Enemy enemy, List<string> fightText, Menu _menuObject)
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


            WeaponAnimation(true, _menuObject);

            EnemyPrint("End fight", 80, 10);
            Print.FightConsole();
            var sounds = _menuObject.SoundList();
            AudioPlaybackEngine sound = new AudioPlaybackEngine();
            sound.PlaySound(sounds[8]);

            FightConsolePrintText(fightText, player, enemy);

            SetTopLeftCursorPosToStandardDragon();
            for (int j = 0; j < dragon.Count; j++)
            {
                Console.SetCursorPosition(left, top);
                Print.Blue(dragon[j]);
                top++;
            }

            Thread.Sleep(500);
            SetTopLeftCursorPosToStandardDragon();
            for (int j = 0; j < dragonFire.Count; j++)
            {
                Console.SetCursorPosition(left, top);
                Print.Red(dragonFire[j]);
                top++;
            }
            Thread.Sleep(500);
            sound.Dispose();
            Print.ClearAllScreen();
            EnemyPrint("End fight", 80, 10);



            player.PrintCurrentPlayerStatus();
            Print.FightConsole();
            SetTopLeftCursorPosToStandardDragon();
            for (int j = 0; j < dragon.Count; j++)
            {
                Console.SetCursorPosition(left, top);
                Print.Blue(dragon[j]);
                top++;
            }

            WeaponAnimation(true, _menuObject);


        }
        //This reads from a file with all enemies in it, and prints out the correct enemy grapics
        public static void EnemyPrint(string keyword)
        {
            /*Check for keyword in file, read from next line and print out 
            til keyword 2 is found (not including keyword2)*/
            string keyWord1 = keyword;
            string keyWord2 = "#" + keyword;
            var contents = File.ReadAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ascii.txt");
            bool printOut = false;
            foreach (var line in contents)
            {
                if (line.Equals(keyWord1) || printOut)
                {
                    if (printOut && line != keyWord2)
                    {
                        Blue(line);
                    }
                    else
                    {
                        if (printOut)
                        {
                            printOut = false;
                        }
                        else
                        {
                            printOut = true;
                        }
                    }
                }
            }

        }
        public static void EnemyPrint(string keyword, int left, int top)
        {
            string keyWord1 = keyword;
            string keyWord2 = "#" + keyword;
            var contents = File.ReadAllLines(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\ascii.txt");
            bool printOut = false;
            foreach (var line in contents)
            {

                if (line.Equals(keyWord1) || printOut)
                {
                    if (printOut && line != keyWord2)
                    {
                        Console.SetCursorPosition(left, top);
                        Blue(line);
                        top++;
                    }
                    else
                    {
                        if (printOut)
                        {
                            printOut = false;
                        }
                        else
                        {
                            printOut = true;
                        }
                    }
                }

            }

        }

        //Printing out a frame in the inventory part
        public static void PrintSplitMenuFrame(int width, int divider)
        {
            top = 10;
            left = 0;

            Console.SetCursorPosition(left, top);
            int height = Console.WindowHeight - (top + 5);
            for (int i = 0; i < height; i++)
            {
                bool completeRow = false;
                Console.SetCursorPosition(left, top);

                for (int j = 0; j < width; j++)
                {
                    if (i == 0 || i == 2 || i == height - 1)
                    {
                        if (i == 0 && j == 0)
                        {
                            Console.Write("╔");
                        }
                        else if (i == 0 && j == divider)

                        {
                            Console.Write("╦");
                        }
                        else if (i == 0 && j == width - 1)
                        {
                            Console.Write("╗");
                        }
                        else if (i == 2 && j == left)
                        {
                            Console.Write("╠");
                        }
                        else if (i == 2 && j == divider)
                        {
                            Console.Write("╬");
                        }
                        else if (i == 2 && j == width - 1)
                        {
                            Console.Write("╣");
                        }
                        else if (i == height - 1 && j == left)
                        {
                            Console.Write("╚");
                        }
                        else if (i == height - 1 && j == divider)
                        {
                            Console.Write("╩");
                        }
                        else if (i == height - 1 && j == width - 1)
                        {
                            Console.Write("╝");
                        }
                        else
                        {
                            Console.Write("═");
                        }
                        completeRow = true;


                    }
                }
                if (!completeRow)
                {
                    Console.Write("║" + new string(' ', divider - 1) + "║" + new string(' ', (width - 2 - divider)) + "║");
                }

                top++;
            }
            top = 10;

        }
        //Printing out a divider in the inventory part when looking at an item or weapon
        public static void PrintHorizontalLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);

            Console.Write(new string('═', 69));



        }
        //Remove the above
        public static void RemoveHorizontalLineArea(int left, int top)
        {

            for (int i = 0; i < 6; i++)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(new string(' ', 69));
                top++;
            }

        }
    }
}
