using RPG_Game.Consumables;
using RPG_Game.Interfaces;
using RPG_Game.Items;
using RPG_Game.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RPG_Game.Gamer
{
    class InventoryGui
    {
        private int top = 0;
        private int left = 0;

        public void InventoryMenu(Player player, Menu _menuObject)
        {

            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default;
            string[] inventoryOptions = new string[3] { "Potions", "Items", "Weapons" };
            do
            {

                Print.ClearAllScreen();
                //Sets the parameters for where the frame should start printing and prints the frame.
                Print.PrintSplitMenuFrame(99, 26);
                Console.SetCursorPosition(4, 11);
                Print.RedW("---- INVENTORY ----");

                Console.SetCursorPosition(62, 11);
                /*****************************************************
                 *               PRINT INVENTORY STATUS                                    *
                 *****************************************************/
                Print.YellowW(player.InventoryStatus());
                List<Weapon> weapons = new List<Weapon>();
                List<Item> items = new List<Item>();
                List<Potion> potions = new List<Potion>();

                foreach (var item in player.PrintAllItems())
                {
                    if (item is Weapon)
                    {
                        weapons.Add((Weapon)item);
                    }
                    else if (item is Item)
                    {
                        items.Add((Item)item);
                    }
                    else if (item is Potion)
                    {
                        potions.Add((Potion)item);
                    }

                }

                /****************************************************************
                 WEAPON PRINTING (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 13;
                left = 28;
                Console.SetCursorPosition(left, top);
                Print.Red("Weapons");
                top++;

                if (weapons.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("You have no weapons");
                }
                foreach (var item in weapons)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(item.Name);
                    top++;
                }

                /****************************************************************
                 ITEM PRINTING (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 24;
                left = 28;

                Console.SetCursorPosition(left, top);
                Print.Red("Items");
                top++;

                if (items.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("You have no items");
                }
                foreach (var item in items)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(item.Name);
                    top++;
                }

                /****************************************************************
                 POTION PRINTING (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 13;
                left = 55;

                Console.SetCursorPosition(left, top);
                Print.Red("Potions");
                top++;

                if (potions.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine("You have no potions");
                }
                foreach (var item in potions)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(item.Name);
                    top++;
                }


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
                Console.SetCursorPosition(left, top);
                Console.WriteLine("B. Back to main menu");
                //Error message is printed out (if there are any)
                if (error)
                {
                    Console.SetCursorPosition(left, top + 3);
                    Print.Red(errorMsg);
                    errorMsg = default;
                }
                Console.SetCursorPosition(left, top + 2);
                Console.Write("Choose your option> ");
                Console.CursorVisible = true;
                option = Console.ReadLine();
                Console.CursorVisible = false;
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.PlaySound(sounds[1]);
                Thread.Sleep(700);
                sound.Dispose();
                switch (option.ToLower())
                {
                    case "1":
                        InventoryMenuPotions(player);

                        break;
                    case "2":
                        InventoryMenuItems(player);


                        break;
                    case "3":

                        break;
                    case "b":
                        continueCode = true;
                        break;

                    default:
                        //If anything else is pressed, errormessage is set.
                        error = true;
                        errorMsg = "Wrong menu choice";
                        break;
                }





            } while (!continueCode);

        }
        private void InventoryMenuPotions(Player player)
        {
            bool continueCode = false;
            
            bool error = false;
            string errorMsg = default;

            do
            {
                top = 11;
                left = 29;
                Console.CursorVisible = true;
                Print.ClearAllScreen(left, top);
                top += 2;
                left = 28;
                Print.ClearAllScreen(left, top);

                Console.SetCursorPosition(29, 11);
                Print.RedW("---- POTIONS ----");




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
                            Print.YellowW($"{i + 1}. {listOfInventory[i].Name} ");
                            Console.Write($"- {listOfInventory[i].Describe()}");
                            topPosition++;
                            if (i == listOfInventory.Count - 1)
                            {
                                topPosition++;
                                Console.SetCursorPosition(leftPosition, topPosition);
                                Console.WriteLine($"B. Go back to inventory");
                                topPosition++;
                                Console.SetCursorPosition(leftPosition, topPosition);
                            }
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine("You have no potions in your inventory. Go to the shop and buy some.");
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"B. Go back to inventory");
                        topPosition++;

                    }
                    Console.CursorVisible = true;
                    Console.SetCursorPosition(29, 11);
                    Print.ClearAllScreen(29, 11);
                    Console.SetCursorPosition(29, 11);
                    Print.RedW("---- POTIONS ----");
                    Console.SetCursorPosition(62, 11);
                    /*****************************************************
                     *               PRINT INVENTORY STATUS                                    *
                     *****************************************************/
                    Print.YellowW(player.InventoryStatus());

                    Console.SetCursorPosition(leftPosition, topPosition);
                    Console.Write("Choose option> ");

                    //Error message is printed out (if there are any)
                    if (error)
                    {
                        Console.SetCursorPosition(leftPosition, topPosition + 1);
                        Console.CursorVisible = false;
                        Print.Red(errorMsg);
                        errorMsg = default;
                    }

                    Console.SetCursorPosition(leftPosition + 15, topPosition);
                    Console.CursorVisible = true;
                    string input = Console.ReadLine();
                    Console.CursorVisible = false;
                    bool successConvert = int.TryParse(input, out int userChoice);
                    if (successConvert && userChoice <= listOfInventory.Count && player.Health < player.MaxHealth)
                    {
                        error = true;
                        errorMsg = player.Consume(listOfInventory[userChoice - 1]);
                        Print.PlayerStatsPrint(player);
                    }
                    else if (!successConvert && input.ToLower() == "b")
                    {
                        continueCode = true;
                    }
                    else
                    {
                        error = true;
                        errorMsg = "Wrong menu choice";

                    }
                    if (player.Health == player.MaxHealth && !error)
                    {
                        error = true;
                        errorMsg = "You have max HP already so you didn´t drink the potion.";
                    }

                } while (!continueCode);

            } while (!continueCode);
            top = 13;
            left = 28;
            Print.ClearAllScreen(left, top);
            left = 45;
        }

        private void InventoryMenuItems(Player player)
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default;
            int temporaryTopCursorPosition = 0;
            int temporaryLeftCursorPosition = 0;

            do
            {
                top = 11;
                left = 29;
                Console.CursorVisible = true;
                Print.ClearAllScreen(left, top);
                top += 2;
                left = 28;
                Print.ClearAllScreen(left, top);

                Console.SetCursorPosition(29, 11);
                Print.RedW("---- ITEMS ----");




                Console.CursorVisible = false;
                List<IInventoryable> returnList = new List<IInventoryable>();
                List<IEquipable> listOfItems = new List<IEquipable>();
                do
                {
                    continueCode = false;
                    int topPosition = 13;
                    int leftPosition = 28;
                    Print.ClearAllScreen(leftPosition, topPosition);
                    returnList.Clear();
                    listOfItems.Clear();

                    returnList = player.PrintAllItems("Item");
                    listOfItems = returnList.Cast<IEquipable>().ToList();

                    if (listOfItems.Count >= 1)
                    {
                        for (int i = 0; i < listOfItems.Count; i++)
                        {
                            Console.SetCursorPosition(leftPosition, topPosition);
                            Print.YellowW($"{i + 1}. {listOfItems[i].Name} ");
                            topPosition++;
                            if (i == listOfItems.Count - 1)
                            {
                                topPosition++;
                                Console.SetCursorPosition(leftPosition, topPosition);
                                Console.WriteLine($"B. Go back to inventory");

                            }
                        }
                        topPosition += 2;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.Write("Choose option> ");
                        temporaryLeftCursorPosition = Console.CursorLeft;
                        temporaryTopCursorPosition = Console.CursorTop;
                        //Error message is printed out (if there are any)
                        if (error)
                        {
                            Console.SetCursorPosition(leftPosition, topPosition + 1);
                            Console.CursorVisible = false;
                            Print.Red(errorMsg);
                            errorMsg = default;
                            error = false;
                            Console.SetCursorPosition(temporaryLeftCursorPosition, temporaryTopCursorPosition);
                            
                        }
                        Console.CursorVisible = true;
                        option = Console.ReadLine();

                        Console.CursorVisible = false;
                        bool okChoice = int.TryParse(option, out int convertedMenuChoice);
                        if (okChoice && convertedMenuChoice <= listOfItems.Count || option.ToLower() == "b")
                        {
                            switch (option.ToLower())
                            {
                                case "1":
                                case "2":
                                case "3":
                                case "4":
                                case "5":
                                case "6":
                                case "7":
                                case "8":
                                case "9":
                                case "10":
                                case "11":
                                case "12":
                                case "13":
                                case "14":
                                case "15":
                                case "16":
                                case "17":
                                case "18":
                                case "19":
                                case "20":

                                    PrintInfoAboutItemOrWeaponInTheMainWindow(listOfItems, option);
                                    break;
                                case "b":
                                    continueCode = true;
                                    break;
                                default:
                                    error = true;
                                    errorMsg = "Wrong menu choice";
                                    continueCode = false;

                                    break;
                            }
                        }
                        else
                        {
                            error = true;
                            errorMsg = "Wrong menu choice";
                            continueCode = false;
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine("You have no Items in your inventory. Go to the shop and buy some.");
                        topPosition += 2;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"B. Go back to inventory");
                        

                    }






                } while (!continueCode);

            } while (!continueCode);
            top = 13;
            left = 28;
            Print.ClearAllScreen(left, top);
            left = 45;
        }

        private void PrintInfoAboutItemOrWeaponInTheMainWindow(List<IEquipable> itemList, string userchoice)
        {
            bool continueCode = false;
            string option = default;
            Print.PrintHorizontalLine(28, 30);
            int topPosition = 31;
            int leftPosition = 28;
            Console.SetCursorPosition(leftPosition, topPosition);
            string name = itemList[Convert.ToInt32(userchoice) - 1].Name;
            string describe = itemList[Convert.ToInt32(userchoice) - 1].Describe();
            Print.Green($"{name} - {describe}");

            topPosition = 30;
            leftPosition = 28;
            Console.SetCursorPosition(leftPosition, topPosition);
            Console.WriteLine("Kod här som ska skriva ut stats om item");
            

            topPosition = 35;
            leftPosition = 28;
            bool error = false;
            string errorMsg = default;
            do
            {
                
                
                if (error)
                {
                    Console.SetCursorPosition(leftPosition, topPosition+1);
                    error = false;
                    Console.WriteLine(errorMsg);
                    errorMsg = default;

                }
                Console.SetCursorPosition(leftPosition, topPosition);
                Console.CursorVisible = true;
                Console.Write("Would you like to equip this? y/n> ");

                option = Console.ReadLine();
                Console.CursorVisible = false;
                switch (option.ToLower())
                {
                    case "y":
                    case "n":
                        continueCode = true;
                        break;
                    default:
                        error = true;
                        errorMsg = "Valid choices y/n";
                        break;
                }

            } while (!continueCode);
            Print.RemoveHorizontalLineArea(28, 30);
        }
    }
}
