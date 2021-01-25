using System;
using System.Text;
using System.Threading;
using RPG_Game.Gamer;
using RPG_Game.Adventure;
using System.Collections.Generic;
using RPG_Game.Statics;
using System.IO;
using System.Reflection;
using RPG_Game.Consumables;
using RPG_Game.Interfaces;
using RPG_Game.TheShop;
using System.Linq;
using System.Diagnostics.Tracing;

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
        
        private string[] inGameMenuOptions = new string[5] { "Go adventure", "Inventory", "Shop", "Save your game", "Exit game" };
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
                
                Print.EnemyPrint("Ending dragon");
                
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
                Console.CursorVisible = true;
                option = Console.ReadLine();
                Console.CursorVisible = false;
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
                    Console.CursorVisible = true;
                    name = Console.ReadLine();
                    Console.CursorVisible = false;
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
                    Print.EnemyPrint("Ending dragon");
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
                        //Front logo on menu screen
                        Print.EnemyPrint("Ending dragon");
                        player.PrintCurrentPlayerStatus();
                        top = 13;
                        for (int i = 0; i < inGameMenuOptions.Length; i++)
                        {
                            Console.SetCursorPosition(left, top);
                            Console.WriteLine($"{i + 1}. {inGameMenuOptions[i]}");
                            top++;
                        }
                        //Reminder for the player to save
                        if (saveReminder >= 5 && saveReminder < 10)
                        {
                            Console.SetCursorPosition(left, top - 7);
                            Print.Yellow("Don´t forget to save your progress!");
                        }
                        else if (saveReminder >= 10)
                        {
                            saveReminder = default(int);
                        }

                        //Error message is printed out (if there are any)
                        if (error)
                        {
                            Console.SetCursorPosition(left, top + 2);
                            Print.Red(errorMsg);
                            errorMsg = default(string);
                        }
                        Console.SetCursorPosition(left, top + 1);
                        Console.Write("Choose your option> ");
                        Console.CursorVisible = true;
                        option = Console.ReadLine(); 
                        Console.CursorVisible = false;
                        var sounds = _menuObject.SoundList();
                        AudioPlaybackEngine sound = new AudioPlaybackEngine();
                        sound.PlaySound(sounds[1]);
                        Thread.Sleep(700);
                        sound.Dispose();
                        switch (option)
                        {
                            case "1":
                                //Go explore
                                error = false;
                                Thread.Sleep(500);
                                Explore explore = new Explore();
                                menuMusic.PauseSound();
                                explore.GoAdventure(player, _menuObject);
                                //If you died, play game over sound.
                                if (!player.Alive)
                                {
                                    
                                    continueCode = true;
                                    var listsound = _menuObject.SoundList();
                                    AudioPlaybackEngine dead = new AudioPlaybackEngine();
                                    dead.PlaySound(listsound[12]);
                                    Print.PlayerStatsPrint(player);
                                    

                                }
                                //else resume menu music and increse reminder counter
                                else
                                {
                                    menuMusic.ResumeSound();
                                    saveReminder++;
                                }
                                break;
                            case "2":
                                InventoryMenu();
                                Console.ReadKey();
                                break;
                            case "3":
                                //Go shop
                                menuMusic.PauseSound();
                                var soundList = _menuObject.SoundList();
                                AudioPlaybackEngine shop = new AudioPlaybackEngine();
                                shop.PlaySound(soundList[2]);

                                Shop theShop = new Shop();
                                theShop.GoIn(player);
                                //shop is the sound for the shop that´s disposing
                                shop.Dispose();
                                menuMusic.ResumeSound();
                                error = false;
                                saveReminder++;
                                break;
                            case "4":
                                //Save your game
                                errorMsg = FileHandling.SavePlayerToFile(playerList);
                                error = true;
                                saveReminder = 0;
                                break;
                            case "5":
                                //Exit game
                                Thread.Sleep(500);
                                Environment.Exit(0);
                                break;
                            default:
                                //If enything else is pressed, errormessage is set.
                                error = true;
                                errorMsg = "Wrong menu choice";
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


        private void InventoryMenu()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);
            int saveReminder = default(int);
            string[] inventoryOptions = new string[4] { "Potions", "Items", "Weapons", "Back to main menu" };
            do
            {
                
                Print.ClearAllScreen();
                //Sets the parameters for where the frame should start printing and prints the frame.
                Print.PrintSplitMenuFrame(99,26);
                Console.SetCursorPosition(4, 11);

                Print.YellowW("---- INVENTORY ----");




                //Sets the parameters for where the menu should start printing.
                top = 13;
                left = 2;
                //prints the menu
                        for (int i = 0; i < inventoryOptions.Length; i++)
                        {

                            Console.SetCursorPosition(left, top);
                            Console.WriteLine($"{i + 1}. {inventoryOptions[i]}");
                            top++;
                        }
                        
                        //Error message is printed out (if there are any)
                        if (error)
                        {
                            Console.SetCursorPosition(left, top + 2);
                            Print.Red(errorMsg);
                            errorMsg = default(string);
                        }
                        Console.SetCursorPosition(left, top + 1);
                        Console.Write("Choose your option> ");
                        Console.CursorVisible = true;
                        option = Console.ReadLine();
                        Console.CursorVisible = false;
                        var sounds = _menuObject.SoundList();
                        AudioPlaybackEngine sound = new AudioPlaybackEngine();
                        sound.PlaySound(sounds[1]);
                        Thread.Sleep(700);
                        sound.Dispose();
                        switch (option)
                        {
                            case "1":
                                InventoryMenuPotions();
                               
                                break;
                            case "2":
                        
                                Console.ReadKey();
                                break;
                            case "3":
                               
                                break;
                            case "4":
                        continueCode = true;
                                break;
                            case "5":
                               
                                break;
                            default:
                                //If anything else is pressed, errormessage is set.
                                error = true;
                                errorMsg = "Wrong menu choice";
                                break;
                        }

                    

                    
                
            } while (!continueCode);

        }





        private void InventoryMenuPotions()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);
            int saveReminder = default(int);
            string[] inventoryOptions = new string[4] { "Potions", "Items", "Weapons", "Back to main menu" };
            do
            {

                Print.ClearAllScreen();
                //Sets the parameters for where the frame should start printing and prints the frame.
                Print.PrintSplitMenuFrame(99, 26);
                Console.SetCursorPosition(4, 11);

                Print.RedW("---- INVENTORY ----");
                Console.SetCursorPosition(29, 11);
                Print.RedW("---- POTIONS ----");


                //Sets the parameters for where the menu should start printing.
                top = 13;
                left = 2;
                //prints the menu
                for (int i = 0; i < inventoryOptions.Length; i++)
                {

                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {inventoryOptions[i]}");
                    top++;
                }

                Console.CursorVisible = false;
                List<IInventoryable> returnList = new List<IInventoryable>();
                List<IConsumable> listOfInventory = new List<IConsumable>();
                do
                {
                    int topPosition = 13;
                    int leftPosition = 28;
                    Print.ClearAllScreen(leftPosition, topPosition);
                    returnList.Clear();
                    listOfInventory.Clear();

                    returnList = player.PrintAllItems("Potion");
                    listOfInventory = returnList.Cast<IConsumable>().ToList();

                    if (listOfInventory.Count >= 1)
                    {
                        for (int i = 0; i < listOfInventory.Count; i++)
                        {
                            Console.SetCursorPosition(leftPosition, topPosition);
                            Print.YellowW($"{i+1}. {listOfInventory[i].Name} ");
                            Console.Write($"- {listOfInventory[i].Describe()}");
                            topPosition++;
                            if (i == listOfInventory.Count - 1)
                            {
                                topPosition++;
                                Console.SetCursorPosition(leftPosition, topPosition);
                                Console.WriteLine($"{i+2}. Go back to inventory");
                                topPosition++;
                                Console.SetCursorPosition(leftPosition, topPosition);

                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine("You have no potions in your inventory. Go to the shop and buy some.");
                    }
                    Console.Write("Choose option> ");
                    topPosition = Console.CursorTop;
                    //Error message is printed out (if there are any)
                    if (error)
                    {
                        Console.SetCursorPosition(leftPosition, topPosition + 1);
                        Console.CursorVisible = false;
                        Print.Red(errorMsg);
                        errorMsg = default(string);
                    }
                    string input = Console.ReadLine();
                    int userChoice;
                    bool successConvert = int.TryParse(input, out userChoice);
                    if (successConvert && userChoice <= listOfInventory.Count)
                    {
                        error = true;
                        errorMsg = player.Consume(listOfInventory[userChoice-1]);
                        Print.PlayerStatsPrint(player);
                    }
                    else if (successConvert && userChoice == listOfInventory.Count + 1)
                    {
                        continueCode = true;
                    }
                    else
                    {
                        error = true;

                    }



                    //Looping as long as the user has not pressed enter
                    
                } while (!continueCode);





            } while (!continueCode);

        }

    }

}
