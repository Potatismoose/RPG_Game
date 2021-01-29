using RPG_Game.Adventure;
using RPG_Game.Gamer;
using RPG_Game.Statics;
using RPG_Game.TheShop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace RPG_Game
{
    //Class that handle the main menues
    class Menu
    {
        private List<Player> playerList = new List<Player>();
        private Player player;
        private int top = 13;
        private int left = 45;
        //Pathway to the save file and file name of the file
        private readonly string pathway = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Saves\\";
        private readonly string file = "playergame.save";

        //Main menu options
        private readonly string[] startMenuOptions = new string[3] { "New game", "Continue your adventure", "Exit game" };
        //in game options
        private readonly string[] inGameMenuOptions = new string[5] { "Go adventure", "Inventory", "Shop", "Save your game", "Exit game" };


        //creating cachedsound  objects, see notes in CachedSound.cs
        private readonly CachedSound menu = new CachedSound(@$"menu.mp3");
        private readonly CachedSound click = new CachedSound(@$"click.mp3");
        private readonly CachedSound shop = new CachedSound(@$"shop.mp3");
        private readonly CachedSound fight = new CachedSound(@$"fight.mp3");
        private readonly CachedSound calm = new CachedSound(@$"calm.mp3");
        private readonly CachedSound levelUp = new CachedSound(@$"levelup.mp3");
        private readonly CachedSound buy = new CachedSound(@$"buy.mp3");
        private readonly CachedSound dragon = new CachedSound(@$"dragon.mp3");
        private readonly CachedSound dragonFire = new CachedSound(@$"dragonfire.mp3");
        private readonly CachedSound refillHP = new CachedSound(@$"refillHP.mp3");
        private readonly CachedSound swordHit = new CachedSound(@$"swordhit.mp3");
        private readonly CachedSound snake = new CachedSound(@$"snake.mp3");
        private readonly CachedSound gameOver = new CachedSound(@$"gameover.mp3");

        //Creates list of sounds and saves sounds to list (see below)
        private readonly List<CachedSound> listOfSounds = new List<CachedSound>();
        //creates the controller for the sound. This is the object that will controll the sound. 
        //Start, stop, dispose, volume and more.
        private readonly AudioPlaybackEngine menuMusic;

        private Menu _menuObject;

        //Constructor of the menu. Adding the sounds to the list.
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

        //Main menu
        public void StartMenu(Menu menuObject)
        {
            _menuObject = menuObject;

            //Start the music
            menuMusic.PlaySound(menu);
            bool continueCode = false;
            bool firstTimeRunningProgram = true;
            string option;
            bool error = false;
            string errorMsg = default;
            do
            {
                //print the logo
                if (firstTimeRunningProgram)
                {
                    Print.LogoPrint();
                    firstTimeRunningProgram = false;
                }
                Print.ClearAllScreen();

                Print.EnemyPrint("Ending dragon");

                top = 13;

                //Print the menu
                for (int i = 0; i < startMenuOptions.Length; i++)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {startMenuOptions[i]}");
                    top++;
                }

                //If there are errors (or messages to the player) they will be printed here.
                if (error)
                {
                    Console.SetCursorPosition(left, top + 2);
                    Print.Red(errorMsg);
                    errorMsg = default;
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
                    //New game
                    case "1":
                        error = false;

                        if (File.Exists(pathway + file))
                        {
                            File.Delete(pathway + file);
                            Directory.Delete(pathway);
                        }
                        int check = FileHandling.CheckFileFolderExistance();
                        if (check == 1 || check == 2)
                        {
                            NewGame();
                        }

                        break;

                    //Continue adventure
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
                    //Exit game
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

        /******************************************************************
                                    NEW GAME MENU   
         ******************************************************************/
        private void NewGame()
        {
            Thread.Sleep(500);

            Print.ClearAllScreen();
            Print.DragonPrint();

            string name;
            bool emptyName = true;
            string errorMsg = default;
            string pathwayFull = string.Concat(pathway, file);
            //If no filepath is found
            if (new FileInfo(pathwayFull).Length == 0)
            {
                do
                {
                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        Console.SetCursorPosition(left, top + 1);
                        Print.Red(errorMsg);
                        errorMsg = default;

                    }

                    //User input for name
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
                    //Check if name is empty
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

                //Creating a new player and saves it to a list
                player = new Player(name);
                playerList.Add(player);
                //Saving the player list to a file.
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
            string errorMsg = default;
            int saveReminder = default;

            do
            {
                //If the player has killed the dragon player will be over 20 strength. 
                //so the check is for if player is back on lvl1 but strength is > 20, 
                //then you have finished the game
                if (player.Level == 1 && player.Strength > 20)
                {
                    Print.ClearAllScreen();
                    Print.EnemyPrint("Ending dragon");
                    Console.SetCursorPosition(left, top);
                    Print.Yellow("You made it til the end, the dragon is defeted. You´re a hero!");
                    Console.ReadKey();
                    continueCode = true;
                }
                //if player is alive
                else
                {
                    if (player.Alive)
                    {


                        Print.ClearAllScreen();
                        //Front logo on menu screen
                        Print.EnemyPrint("Ending dragon");
                        player.PrintCurrentPlayerStatus();
                        top = 13;
                        left = 45;
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
                            saveReminder = default;
                        }

                        //Error message is printed out (if there are any)
                        if (error)
                        {
                            Console.SetCursorPosition(left, top + 2);
                            Print.Red(errorMsg);
                            errorMsg = default;
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
                                InventoryGui inventory = new InventoryGui();
                                inventory.InventoryMenu(player, _menuObject);

                                break;
                            case "3":
                                //Go shop
                                menuMusic.PauseSound();
                                var soundList = _menuObject.SoundList();
                                AudioPlaybackEngine shopMusic = new AudioPlaybackEngine();
                                shopMusic.PlaySound(soundList[2]);

                                Shop theShop = new Shop(_menuObject, player);
                                theShop.GoIn(player);
                                //shop is the sound for the shop that´s disposing
                                shopMusic.Dispose();
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
                                //If anything else is pressed, errormessage is set.
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


    }

}
