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
        private List<Player> playerList = new List<Player>();
        private Player player;
        private int top = 13;
        private int left = 45;
        private string pathway = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Saves\\";
        private string file = "playergame.save";

        private string[] startMenuOptions = new string[3] { "New game", "Continue your adventure", "Exit game" };
        private string[] shopOptions = new string[4] { "Health Potion", "Armor", "Strength Amulett", "Back to your adventure" };
        private string[] inGameMenuOptions = new string[4] { "Go adventure", "Shop", "Save your game", "Exit game" };
        private CachedSound menu = new CachedSound(@$"menu.mp3");
        private CachedSound click = new CachedSound(@$"click.mp3");
        private CachedSound shop = new CachedSound(@$"shop.mp3");
        private CachedSound fight = new CachedSound(@$"fight.mp3");
        private CachedSound calm = new CachedSound(@$"calm.mp3");
        private CachedSound levelUp = new CachedSound(@$"levelup.mp3");
        private CachedSound buy = new CachedSound(@$"buy.mp3");
        private CachedSound dragon = new CachedSound(@$"dragon.mp3");
        private CachedSound dragonFire = new CachedSound(@$"dragonfire.mp3");
        private CachedSound refillHP = new CachedSound(@$"refillHP.mp3");
        private CachedSound swordHit = new CachedSound(@$"swordhit.mp3");
        private CachedSound snake = new CachedSound(@$"snake.mp3");
        private CachedSound gameOver = new CachedSound(@$"gameover.wav");


        private List<CachedSound> listOfSounds = new List<CachedSound>();
        private AudioPlaybackEngine menuMusic;
        
        private Menu _menuObject;

        public Menu()
        {
            menuMusic = new AudioPlaybackEngine();
            
            listOfSounds.Add(menu);
            listOfSounds.Add(click);
            listOfSounds.Add(shop);
            listOfSounds.Add(fight);
            listOfSounds.Add(calm);
            listOfSounds.Add(levelUp);
            listOfSounds.Add(buy);
            listOfSounds.Add(dragon);
            listOfSounds.Add(dragonFire);
            listOfSounds.Add(refillHP);
            listOfSounds.Add(swordHit);
            listOfSounds.Add(snake);
            listOfSounds.Add(gameOver);

        }
        public List<CachedSound> SoundList()
        {
            return listOfSounds;
        }
        //Huvudmenyn
        public void StartMenu(Menu menuObject)
        {
            _menuObject = menuObject;


            menuMusic.PlaySound(menu);
            bool continueCode = false;
            bool firstTimeRunningProgram = true;
            string option;
            bool error = false;
            string errorMsg = default(string);
            do
            {
                
                if (firstTimeRunningProgram)
                {
                    Print.LogoPrint();
                    firstTimeRunningProgram = false;
                }
                Print.ClearAllScreen();
                Print.DragonPrint();
                
                top = 13;
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
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.PlaySound(sounds[1]);
                Thread.Sleep(700);
                sound.Dispose();
                switch (option)
                {
                    case "1":
                        error = false;
                        
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

        //Nytt spel menyn
        private void NewGame()
        {
            Thread.Sleep(500);
            
            Print.ClearAllScreen();
            Print.DragonPrint();
            
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
                    Print.SetTopLeftCursorPosToStandard();
                    Console.Write("What is our heros name?> ");

                    name = Console.ReadLine();
                    var sounds = _menuObject.SoundList();
                    AudioPlaybackEngine sound = new AudioPlaybackEngine();
                    sound.PlaySound(sounds[1]);
                    Thread.Sleep(700);
                    sound.Dispose();
                    if (!string.IsNullOrEmpty(name))
                    {
                        emptyName = false;
                    }
                    else
                    {
                        top = 13;
                        Console.SetCursorPosition(left, top + 1);
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

        //In game menyn
        private void InGameMenu()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);
            int saveReminder = default(int);

            do
                {
                if (player.Level == 10)
                {
                    Print.ClearAllScreen();
                    Print.DragonPrint();
                    Console.SetCursorPosition(left, top);
                    Print.Yellow("You made it til the end, the dragon is defeted. You´re a hero!");
                    Console.ReadKey();
                    continueCode = true;
                }
                else
                {
                    if (player.Alive)
                    {



                        Print.ClearAllScreen();
                        Print.DragonPrint();
                        player.PrintCurrentPlayerStatus();
                        top = 13;
                        for (int i = 0; i < inGameMenuOptions.Length; i++)
                        {
                            Console.SetCursorPosition(left, top);
                            Console.WriteLine($"{i + 1}. {inGameMenuOptions[i]}");
                            top++;
                        }
                        if (saveReminder >= 5 && saveReminder < 10)
                        {
                            Console.SetCursorPosition(left, top - 7);
                            Print.Yellow("Don´t forget to save your progress!");
                        }
                        else if (saveReminder >= 10)
                        {
                            saveReminder = default(int);
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
                        var sounds = _menuObject.SoundList();
                        AudioPlaybackEngine sound = new AudioPlaybackEngine();
                        sound.PlaySound(sounds[1]);
                        Thread.Sleep(700);
                        sound.Dispose();
                        switch (option)
                        {
                            case "1":

                                error = false;
                                Thread.Sleep(500);
                                Explore explore = new Explore();
                                menuMusic.PauseSound();


                                explore.GoAdventure(player, _menuObject);
                                if (!player.Alive)
                                {
                                    
                                    continueCode = true;
                                    var listsound = _menuObject.SoundList();
                                    AudioPlaybackEngine dead = new AudioPlaybackEngine();
                                    dead.PlaySound(listsound[12]);
                                    Print.PlayerStatsPrint(player);
                                    

                                }
                                else
                                {
                                    menuMusic.ResumeSound();
                                    saveReminder++;
                                }
                                break;
                            case "2":

                                menuMusic.PauseSound();
                                var soundList = _menuObject.SoundList();
                                AudioPlaybackEngine shop = new AudioPlaybackEngine();
                                shop.PlaySound(soundList[2]);


                                ShopMain();
                                shop.Dispose();
                                menuMusic.ResumeSound();
                                error = false;
                                saveReminder++;
                                break;
                            case "3":

                                errorMsg = FileHandling.SavePlayerToFile(playerList);
                                error = true;
                                saveReminder = 0;
                                break;
                            case "4":

                                Thread.Sleep(500);
                                Environment.Exit(0);
                                break;
                            default:
                                error = true;
                                errorMsg = "Wrong menu choice";
                                Console.WriteLine("");
                                break;
                        }

                    }

                    else
                    {
                        continueCode = true;
                    }
                }
            } while (!continueCode);
            
        }

        //Shop main
        private void ShopMain()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            do
            {
                //Console.Clear();
                //Print.LogoPrint();
                Print.ClearAllScreen();
                Print.DragonPrint();
                player.PrintCurrentPlayerStatus();
                Print.SetTopLeftCursorPosToStandard();
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
