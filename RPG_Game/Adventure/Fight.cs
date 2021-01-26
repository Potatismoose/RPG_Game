using RPG_Game.Enemies;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPG_Game.Adventure
{   [Serializable]
    class Fight
    {
        
        Player _player;
        List<string> _fightText;
        public Fight(Player player)
        {
            _player = player;
        }

        public Player Player { get; }
        
        public void PrintFight(Enemy enemy, Player player, AudioPlaybackEngine currentMusic, Menu _menuObject)
        {
            List<IConsumable> listOfPotions;
            List<string> userInteractions = new List<string>() { "I - Inventory", "ENTER - Attack again/Continue", "ESC - Close inventory" };
            int top = 11;
            int left = 0;
            int leftMoveHere = default(int);
            int topMoveHere = default(int);
            List<string> fightText = new List<string>();
            _fightText = fightText;
            do
            {
                fightText.Clear();
                if (player.Alive == true)
                {

                                                        
                    if (leftMoveHere != 0)
                    {
                        Print.ClearAllScreen(leftMoveHere,topMoveHere);
                    }
                    else { Print.ClearAllScreen(); }

                    if (enemy.ToString() == "Dragon")
                    {
                        Print.DragonPrint();
                    }
                    else 
                    {
                        Print.EnemyPrint(enemy.Type);
                    }

                    if (enemy.IsBoss && enemy.Type != "Dragon")
                    {
                        Print.EnemyPrint("Boss fight", 80, 10);
                    }
                    else if (enemy.IsBoss)
                    {
                        Print.EnemyPrint("End fight", 80, 10);
                    }
                    player.PrintCurrentPlayerStatus();
                    Print.FightConsole();
                    Print.FightConsolePrintText(fightText, player, enemy);
                    leftMoveHere = 0;
                    topMoveHere = 40;
                    Console.SetCursorPosition(leftMoveHere, topMoveHere);
                    for (int i = 0; i < userInteractions.Count; i++)
                    {
                        Console.Write($"  ║  {userInteractions[i]}  ║      ");

                    }
                    Console.SetCursorPosition(left, top);
                    leftMoveHere = 0;
                        topMoveHere = 40;
                        Console.SetCursorPosition(leftMoveHere, topMoveHere);
                        for (int i = 0; i < userInteractions.Count; i++)
                        {
                                Console.Write($"  ║  {userInteractions[i]}  ║      ");
                            
                        }
                    if (enemy.ToString() == "Dragon")
                    {
                        Print.WeaponAnimation(false, _menuObject);
                    }
                    
                    fightText.Add(player.Attack(enemy));
                    Print.FightConsolePrintText(fightText, player, enemy);
                    leftMoveHere = Console.CursorLeft;
                    topMoveHere = Console.CursorTop;
                    


                    Thread.Sleep(1000);
                    if (enemy.Alive)
                    {
                        if (enemy.ToString() == "Dragon")
                        {
                            Print.ClearAllScreen();
                            player.PrintCurrentPlayerStatus();
                            Print.FightConsolePrintText(fightText, player, enemy);
                            Print.DragonAnimation(player, enemy, fightText, _menuObject);
                        }
                        
                        fightText.Add(enemy.Attack(player));
                        Print.FightConsolePrintText(fightText, player, enemy);
                        leftMoveHere = Console.CursorLeft;
                        topMoveHere = Console.CursorTop;
                        if (player.Alive)
                        {


                            Print.PlayerStatsPrint(player);

                        }
                        else
                        {
                            
                            currentMusic.PauseSound();
                        }


                        Console.SetCursorPosition(leftMoveHere, topMoveHere);

                        
                        
                            
                            
                            
                        
                        leftMoveHere = default(int);
                        topMoveHere = default(int);
                        ConsoleKey key3;
                        
                        int topPosition = 18;
                        int leftPosition = 111;
                        do
                        {
                            topPosition = 0;
                            Console.SetCursorPosition(leftPosition, topPosition);
                            leftMoveHere = 0;
                            topMoveHere = 40;
                            Console.SetCursorPosition(leftMoveHere, topMoveHere);
                            for (int i = 0; i < userInteractions.Count; i++)
                            {
                                Console.Write($"  ║  {userInteractions[i]}  ║      ");

                            }
                            //Start listning for keypress after fight
                            Console.CursorVisible = false;
                            var keytest = Console.ReadKey(true);
                            key3 = keytest.Key;
                            bool closedInventory = false;
                            //if pressed key is I, then print out players inventory
                            if (key3 == ConsoleKey.I)
                            {
                                listOfPotions = player.PrintAllItems(0);

                                bool treatEscapeAsCancel = true;
                                string inputString;
                                bool hideInput = false;

                                Console.CursorVisible = true;

                                StringBuilder inputBuilder = new StringBuilder();
                                //While player has not pressed escape
                                bool error = false;
                                string errorMsg = default(string);
                                
                                while (true)
                                {
                                
                                    topPosition = 18;
                                    Console.SetCursorPosition(leftPosition, topPosition);
                                    Print.Red("Inventory (press nr to use)");
                                    topPosition++;
                                    
                                    int counter = 1;
                                    if (listOfPotions.Count < 1)
                                    {
                                        Console.SetCursorPosition(leftPosition, topPosition);
                                        Console.WriteLine("Out of potions. Press ESC");
                                        Console.CursorVisible = false;
                                    }
                                    else
                                    {
                                        foreach (var item in listOfPotions)
                                        {

                                            Console.SetCursorPosition(leftPosition, topPosition);
                                            Console.WriteLine($"{counter}. {item.Name} +{item.TheChange}");
                                            topPosition++;
                                            counter++;

                                        }
                                    }

                                    topPosition += 2;
                                    if (error)
                                    {
                                        Console.SetCursorPosition(leftPosition, topPosition);
                                        Print.Red(errorMsg);
                                        error = false;
                                        errorMsg = default(string);
                                    }topPosition--;
                                    
                                    Console.SetCursorPosition(leftPosition, topPosition);
                                    if (listOfPotions.Count != 0)
                                    {
                                        Console.CursorVisible = true;
                                        Console.Write("Choose a potion: ");
                                    }
                                    Console.SetCursorPosition(leftPosition+17+inputBuilder.Length, topPosition);
                                    ConsoleKeyInfo inputKey = Console.ReadKey(true);
                                    if (inputKey.Key == ConsoleKey.Enter)
                                    {
                                        inputString = inputBuilder.ToString();

                                        int number;
                                        bool converted = int.TryParse(inputString, out number);
                                        if (converted)
                                        {
                                            if (number <= listOfPotions.Count)
                                            {
                                                if (listOfPotions[number - 1].Name == "Healing potion" || listOfPotions[number - 1].Name == "Max healing potion" && player.Health != player.MaxHealth)
                                                {
                                                    player.RestoreHp(listOfPotions[number - 1].TheChange);
                                                    player.RemoveFromBackpack((IInventoryable)listOfPotions[number - 1]);
                                                    listOfPotions.Remove(listOfPotions[number - 1]);
                                                }
                                                
                                                else if (listOfPotions[number - 1].Name == "Magic agility potion")
                                                {
                                                    string beenDrinking = player.SetAgilityTempUp(listOfPotions[number - 1].Consume());
                                                    if (string.IsNullOrEmpty(beenDrinking))
                                                    {
                                                        player.RemoveFromBackpack((IInventoryable)listOfPotions[number - 1]);
                                                        listOfPotions.Remove(listOfPotions[number - 1]);
                                                    }
                                                    else {
                                                        Console.SetCursorPosition(leftPosition, topPosition);
                                                        Console.WriteLine(beenDrinking);
                                                        Console.ReadLine();
                                                    }
                                                }
                                                topPosition = 18;
                                                Print.ClearAllScreen(leftPosition, topPosition);
                                                Print.ClearAllScreen(104, 0);
                                                inputBuilder.Clear();
                                                player.PrintCurrentPlayerStatus();


                                            }
                                            else 
                                            {
                                                error = true;
                                                errorMsg = "You don´t have that";
                                                inputBuilder.Clear();
                                            }
                                        }
                                        else
                                        {
                                            error = true;
                                            errorMsg = "Not a valid choice.";
                                            inputBuilder.Clear();
                                            topPosition = 18;
                                            Console.SetCursorPosition(leftPosition, topPosition);
                                            Print.ClearAllScreen(leftPosition, topPosition);
                                            
                                            //Printing out everything again so it looks clean.
                                            Print.EnemyPrint(enemy.Type, 0, 10);
                                            
                                           
                                            topPosition = 18;

                                        }



                                    }
                                    //if player presses escape, then break the loop.
                                    else if (treatEscapeAsCancel && inputKey.Key == ConsoleKey.Escape)
                                    {
                                        inputString = null;
                                        break;
                                    }
                                    else
                                        inputBuilder.Append(inputKey.KeyChar);

                                    if (!hideInput)
                                        Console.Write(inputKey.KeyChar);
                                }
                                //Cleaning up leftovers from inventory
                                topPosition = 18;
                                Print.ClearAllScreen(leftPosition, topPosition);
                                //Printing out everything again so it looks clean.
                                
                                
                                
                                

                            }

                            //Looping as long as the user has not pressed enter
                        } while (key3 != ConsoleKey.Enter);
                        
                    }
                    else
                    {
                        
                        currentMusic.PauseSound();
                        currentMusic.Dispose();
                        Console.SetCursorPosition(0, 35);
                        Print.Green("VICTORY!!!");
                        player.SetAgilityTempUp(0);
                        Print.Green($"You looted the enemy and got {player.TakeGold(enemy.DropGold())} gold");
                        int currentLevel = player.Level;
                        Print.Green($"You also got {player.TakeXp(enemy.GiveXp(), _menuObject)} XP");
                        
                        CachedSound win = new CachedSound(@$"fightwin.mp3");
                        AudioPlaybackEngine fightWin;
                        fightWin = new AudioPlaybackEngine();
                        fightWin.PlaySound(win);
                        Thread.Sleep(4000);
                        fightWin.Dispose();


                        if (currentLevel < player.Level)
                        {
                            Random rand = new Random();
                            
                            Console.Write($"LEVEL UP! You got level up bonus gold. Press enter to continue.");
                            leftMoveHere = Console.CursorLeft;
                            topMoveHere = Console.CursorTop;
                            var sounds = _menuObject.SoundList();
                            AudioPlaybackEngine sound = new AudioPlaybackEngine();
                            sound.PlaySound(sounds[5]);
                            Print.PlayerStatsPrint(player);
                            Thread.Sleep(500);
                            sound.Dispose();
                            
                        }
                        else {
                            Console.Write("Press enter to continue.");
                            leftMoveHere = Console.CursorLeft;
                            topMoveHere = Console.CursorTop;
                            Print.PlayerStatsPrint(player);
                            
                            
                        }

                        Console.ReadKey();
                        break;



                    }

                }
                else
                {
                    break;
                }
            } while (enemy.Alive);
            
            



        }


    }
}
