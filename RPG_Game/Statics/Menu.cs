using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using NAudio.Wave;
using System.Threading;
using RPG_Game.Gamer;
using RPG_Game.Soundhandling;
using RPG_Game.Adventure;

namespace RPG_Game
{
     class Menu
    {
        Player player;
        private int top = 13;
        private int left = 45;
        string[] startMenuOptions = new string[3] { "New game", "Continue your adventure", "Exit game" };
        string[] inGameMenuOptions = new string[3] { "Go adventure", "Shop", "Exit game" };

        public StartSound sounds { get; }
        StartSound soundInitilizer = new StartSound();
        public Menu(StartSound sounds)
        {
            this.sounds = sounds;
        }
        private void SetTopLeftCursorPosToStandard()
        {
            top = 13;
            left = 45;
        }
        public void StartMenu()
        {
            
            
            AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Menu);  
            bool continueCode = false;
            string option;
            bool error = false;
            do
            {
                Console.Clear();
                Print.LogoPrint();
                Print.DragonPrint();
                SetTopLeftCursorPosToStandard();
                for (int i = 0; i < startMenuOptions.Length; i++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {startMenuOptions[i]}");
                    top++;
                }
                if (error)
                {
                    Console.SetCursorPosition(left, top + 2);
                    Print.Red("Wrong menu choice");
                }
                Console.SetCursorPosition(left, top + 1);
                Console.Write("Choose your option> ");
                
                 option = Console.ReadLine();
                AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Click);
                switch (option)
                {
                    case "1":
                        error = false;
                        NewGame();
                        break;
                    case "2":
                        error = false;
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        error = true;
                        Console.WriteLine("");
                        break;
                }

                
            } while (!continueCode);

            
        }

        private void NewGame()
        {
            Thread.Sleep(500);
            Console.Clear();
            Print.LogoPrint();
            Print.DragonPrint();
            SetTopLeftCursorPosToStandard();

            Console.SetCursorPosition(left, top);
            Console.Write("What is our heros name?> ");
            string name = Console.ReadLine();
            AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Click);
            player = new Player(name);

            PrintCurrentStatus();
            InGameMenu();
        }

        private void InGameMenu()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            do
            {
                Console.Clear();
                Print.LogoPrint();
                Print.DragonPrint();
                SetTopLeftCursorPosToStandard();
                for (int i = 0; i < inGameMenuOptions.Length; i++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {inGameMenuOptions[i]}");
                    top++;
                }
                if (error)
                {
                    Console.SetCursorPosition(left, top + 2);
                    Print.Red("Wrong menu choice");
                }
                Console.SetCursorPosition(left, top + 1);
                Console.Write("Choose your option> ");
                PrintCurrentStatus();
                option = Console.ReadLine();
                AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Click);
                switch (option)
                {
                    case "1":
                        error = false;
                        Explore explore = new Explore();
                        explore.GoAdventure(player);
                        break;
                    case "2":
                        error = false;
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        error = true;
                        Console.WriteLine("");
                        break;
                }


            } while (!continueCode);
        }

        private void PrintCurrentStatus()
        {
            top = 1;
            left = 105;
            var status = player.ShowCurrentStatus();

            StringBuilder str = new StringBuilder();
            str.Append("*");
            str.Append(' ', 25);
            str.Append("*");
            int extraRows = 3;

            for (int i = 0; i < status.Count + extraRows; i++)
            {
                if (i == 0 || i == status.Count + extraRows - 1)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("***************************");
                }
                else {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(str.ToString());
                }
                top++;
            }

            top = 2;
            left = 107;
            Console.SetCursorPosition(left, top);
            Console.WriteLine($"Name: {player.Name}");


            top = 3;
            left = 107;
            foreach (var item in status)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{item.Key}: {item.Value}");
                top++;
            }
        }

        
    }

}
