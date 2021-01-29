using RPG_Game.Enemies;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPG_Game.Adventure
{
    [Serializable]
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

            //initializing some things for the fight
            List<IConsumable> listOfPotions;
            List<string> userInteractions = new List<string>() { "I - Inventory", "ENTER - Attack again/Continue", "ESC - Close inventory", "F - Flee" };
            int top = 11;
            int left = 0;
            int leftMoveHere = default;
            int topMoveHere = default;
            List<string> fightText = new List<string>();
            _fightText = fightText;
            bool fled = false;
            do
            {
                fightText.Clear();
                if (player.Alive)
                {


                    if (leftMoveHere != 0)
                    {
                        Print.ClearAllScreen(leftMoveHere, topMoveHere);
                    }
                    else { Print.ClearAllScreen(); }
                    //If the enemy is the end boss, print out a dragon as a picture.
                    if (enemy.ToString() == "Dragon")
                    {
                        Print.DragonPrint();
                    }
                    else
                    {//else print out the current enemy
                        Print.EnemyPrint(enemy.Type);
                    }
                    //else if miniboss, print out miniboss.
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
                        Console.Write($" ║  {userInteractions[i]}  ║   ");

                    }
                    Console.SetCursorPosition(left, top);
                    leftMoveHere = 0;
                    topMoveHere = 40;
                    Console.SetCursorPosition(leftMoveHere, topMoveHere);
                    for (int i = 0; i < userInteractions.Count; i++)
                    {
                        Console.Write($" ║  {userInteractions[i]}  ║   ");

                    }

                    //If endboss, do a weapon animation 
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
                        //if the enemy is alive after the attack, counter attack (and do animation if it applies)
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
                        ConsoleKey key3;
                        int topPosition;
                        int leftPosition;

                        //The do loop below controls the in fight inventorysystem an keylistners for that to work.
                        do
                        {

                            leftPosition = 0;
                            topPosition = 40;
                            Console.SetCursorPosition(leftPosition, topPosition);
                            for (int i = 0; i < userInteractions.Count; i++)
                            {
                                Console.Write($" ║  {userInteractions[i]}  ║   ");

                            }
                            //Start listning for keypress after fight
                            Console.CursorVisible = false;
                            var keytest = Console.ReadKey(true);
                            key3 = keytest.Key;

                            //if pressed key is I, then print out players inventory, and if F, flee.
                            if (key3 == ConsoleKey.F)
                            {
                                fled = true;
                                break;
                            }
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
                                string errorMsg = default;
                                //While player have not aborted inventory with escape
                                //Player has access to potions during fight
                                while (true)
                                {

                                    topPosition = 18;
                                    leftPosition = 111;
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
                                        errorMsg = default;
                                    }
                                    topPosition--;

                                    Console.SetCursorPosition(leftPosition, topPosition);
                                    if (listOfPotions.Count != 0)
                                    {
                                        Console.CursorVisible = true;
                                        Console.Write("Choose a potion: ");
                                    }
                                    Console.SetCursorPosition(leftPosition + 17 + inputBuilder.Length, topPosition);
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
                                                    else
                                                    {
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
                        //play victory sound and print out victory text
                        currentMusic.PauseSound();
                        currentMusic.Dispose();
                        Console.SetCursorPosition(0, 35);
                        Print.Green("VICTORY!!!");
                        player.SetAgilityTempUp(0);
                        Print.Green($"You looted the enemy and got {player.TakeGold(enemy.DropGold())} gold");
                        int currentLevel = player.Level;
                        Print.Green($"You also got {player.TakeXp(enemy.GiveXp())} XP");

                        CachedSound win = new CachedSound(@$"fightwin.mp3");
                        AudioPlaybackEngine fightWin;
                        fightWin = new AudioPlaybackEngine();
                        fightWin.PlaySound(win);
                        Thread.Sleep(4000);
                        fightWin.Dispose();

                        //if level up, play sound and do an update of player stats
                        if (currentLevel < player.Level)
                        {
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
                        //update player stats
                        else
                        {
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
                //if the player fled or the enemy died, the fight is over.
            } while (enemy.Alive && !fled);





        }


    }
}
