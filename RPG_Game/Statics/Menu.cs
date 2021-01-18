using System;
using System.Text;
using System.Threading;
using RPG_Game.Gamer;
using RPG_Game.Adventure;
using System.Collections.Generic;
using RPG_Game.Statics;
using System.IO;
using System.Reflection;

namespace RPG_Game
{
     class Menu
    {
        List<Player> playerList = new List<Player>();
        Player player;
        private int top = 13;
        private int left = 45;
        string pathway = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Saves\\";
        string file = "playergame.save";
        
        string[] startMenuOptions = new string[3] { "New game", "Continue your adventure", "Exit game" };
        string[] shopOptions = new string[4] { "Health Potion", "Armor", "Strength Amulett", "Back to your adventure" };
        string[] inGameMenuOptions = new string[4] { "Go adventure", "Shop", "Save your game", "Exit game" };
        CachedSound menu = new CachedSound(@$"menu.mp3");
        CachedSound click = new CachedSound(@$"click.mp3");
        CachedSound shop = new CachedSound(@$"shop.mp3");
        CachedSound fight = new CachedSound(@$"fight.mp3");

        AudioPlaybackEngine menuMusic;
        AudioPlaybackEngine userEnterPress;
        AudioPlaybackEngine shopMusic;
        

        public Menu()
        {
            menuMusic = new AudioPlaybackEngine();
            userEnterPress = new AudioPlaybackEngine();
            shopMusic = new AudioPlaybackEngine();
            

        }
        private void SetTopLeftCursorPosToStandard()
        {
            top = 13;
            left = 45;
        }
        public void StartMenu()
        {


            menuMusic.PlaySound(menu);
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);
            do
            {
                Console.Clear();
                Print.LogoPrint();
                Print.DragonPrint();
                SetTopLeftCursorPosToStandard();
                for (int i = 0; i < startMenuOptions.Length; i++)
                {
                            Console.SetCursorPosition(left, top);
                            Console.WriteLine($"{i+1}. {startMenuOptions[i]}");
                            top++;
                }
                if (error)
                {
                    Console.SetCursorPosition(left, top + 2);
                    Print.Red(errorMsg);
                    errorMsg = default(string);
                }
                Console.SetCursorPosition(left, top + 1);
                Console.Write("Choose your option> ");
                
                 option = Console.ReadLine();
               
                switch (option)
                {
                    case "1":
                        error = false;
                        userEnterPress.PlaySound(click);
                        if (File.Exists(pathway + file))
                        {
                            File.Delete(pathway + file);
                            Directory.Delete(pathway);
                        }
                        int check = FileHandling.CheckFileFolderExistance();
                        if ( check == 1 || check == 2)
                        {
                            NewGame();
                        }
                        
                        break;
                    case "2":
                        string pathwayFull = string.Concat(pathway, file);
                        userEnterPress.PlaySound(click);


                        if (FileHandling.CheckFileFolderExistance() == 1 || FileHandling.CheckFileFolderExistance() == 2)
                        {
                            
                                error = true;
                                errorMsg = "No previous games saved";

                            
                            
                        }
                        else
                        {
                            if (new FileInfo(pathwayFull).Length == 0)
                            {
                                error = true;
                                errorMsg = "No previous games saved";

                            }
                            else
                            {
                                playerList = FileHandling.BinaryDeSerializer(playerList);
                                error = false;
                                NewGame();
                            }
                        }
                        
                        
                        
                        break;
                    case "3":


                        userEnterPress.PlaySound(click);
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        error = true;

                        errorMsg = "Wrong menu choice";
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
            string name;
            bool emptyName = true;
            string errorMsg = default(string);
            string pathwayFull = string.Concat(pathway, file);
            if (new FileInfo(pathwayFull).Length == 0)
            {
                do
                {
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        Console.SetCursorPosition(left, top + 1);
                        Print.Red(errorMsg);
                        errorMsg = default(string);

                    }
                    Console.SetCursorPosition(left, top);
                    Console.Write("What is our heros name?> ");

                    name = Console.ReadLine();
                    userEnterPress.PlaySound(click);
                    if (!string.IsNullOrEmpty(name))
                    {
                        emptyName = false;
                    }
                    else
                    {
                        errorMsg = "Name can not be empty";
                    }
                } while (emptyName);

                player = new Player(name);
                playerList.Add(player);
                FileHandling.BinarySerializer(playerList);
            }
            else
            {
                foreach (var item in playerList)
                {
                    player = item;
                }
            }
            player.PrintCurrentPlayerStatus();
            InGameMenu();

            
        }

        private void InGameMenu()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);

            do
                {
                if (player.Alive)
                {
                    
                    Console.Clear();
                    Print.LogoPrint();
                    Print.DragonPrint();
                    player.PrintCurrentPlayerStatus();
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
                        Print.Red(errorMsg);
                        errorMsg = default(string);
                    }
                    Console.SetCursorPosition(left, top + 1);
                    Console.Write("Choose your option> ");

                    option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                            error = false;

                            Explore explore = new Explore();
                            menuMusic.PauseSound();


                            explore.GoAdventure(player, new AudioPlaybackEngine(), fight);

                            menuMusic.ResumeSound();
                            break;
                        case "2":
                            menuMusic.PauseSound();
                            shopMusic.PlaySound(shop);
                            Shop();
                            shopMusic.PauseSound();
                            menuMusic.ResumeSound();
                            error = false;
                            break;
                        case "3":
                            FileHandling.BinarySerializer(playerList);
                            error = true;
                            errorMsg = "Game saved";
                            break;
                        case "4":
                            Environment.Exit(0);
                            break;
                        default:
                            error = true;
                            errorMsg = "Wrong menu choice";
                            Console.WriteLine("");
                            break;
                    }

                }
                else {
                    continueCode = true;
                }
            } while (!continueCode);
            
        }


        private void Shop()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            do
            {
                Console.Clear();
                Print.LogoPrint();
                Print.DragonPrint();
                player.PrintCurrentPlayerStatus();
                SetTopLeftCursorPosToStandard();
                for (int i = 0; i < shopOptions.Length; i++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {shopOptions[i]}");
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
                
                switch (option)
                {
                    case "1":
                        error = false;
                        
                        break;
                    case "2":
                        error = false;
                        break;
                    case "3":
                        
                        break;
                    case "4":
                        continueCode = true;
                        break;
                    default:
                        error = true;
                        Console.WriteLine("");
                        break;
                }


            } while (!continueCode);
        }

       

        
    }

}
