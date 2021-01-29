using RPG_Game.Interfaces;
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
        private List<IWeapon> weapons = new List<IWeapon>();
        private List<IItem> items = new List<IItem>();
        private List<IConsumable> potions = new List<IConsumable>();
        private List<IEquippable> yourEquippableEquipment = new List<IEquippable>();
        private string itemOrWeapon = default;


        /**************************************************
                    START OF MAIN INVENTORY MENU
         **************************************************
         £££££££££££££££££££££££££££££££££££££££££££££££££££
            £££££££££££££££££££££££££££££££££££££££££££££
                £££££££££££££££££££££££££££££££££££££
                    £££££££££££££££££££££££££££££
                        £££££££££££££££££££££
                            £££££££££££££
                                £££££
                                  £
         */

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
                 *          ADD INVENTORY STATUS TO LISTS            *
                 *****************************************************/
                Print.YellowW(player.InventoryStatus());

                CreateListOfAllInventory(player);


                /****************************************************************
                 WEAPON PRINTING ON FIRST INVENTORY PAGE (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 13;
                left = 28;
                Console.SetCursorPosition(left, top);
                Print.Red("Weapons");
                top++;

                if (weapons.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Print.DarkGrey("You have no weapons");
                }
                foreach (var item in weapons)
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write($"{item.Name} ");
                    if (item.Equipped == true)
                    {
                        Print.Green($"Equipped");
                    }
                    top++;
                }

                /****************************************************************
                 ITEM PRINTING ON FIRST INVENTORY PAGE (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 24;
                left = 28;

                Console.SetCursorPosition(left, top);
                Print.Red("Items");
                top++;

                if (items.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Print.DarkGrey("You have no items");
                }
                foreach (var item in items)
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write($"{item.Name} ");
                    if (item.Equipped == true)
                    {
                        Print.Green($"Equipped");
                    }
                    top++;
                }

                /****************************************************************
                 POTION PRINTING ON FIRST INVENTORY PAGE (SUMMARY OVER INVENTORY) 
                 ****************************************************************/
                top = 13;
                left = 55;

                Console.SetCursorPosition(left, top);
                Print.Red("Potions");

                top++;

                if (potions.Count < 1)
                {
                    Console.SetCursorPosition(left, top);
                    Print.DarkGrey("You have no potions");
                }
                foreach (var item in potions)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine(item.Name);
                    top++;
                }

                /***********************************************************
                        MAIN INVENTORY MENU IS PRINTED BELOW 
                 ***********************************************************/
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
                //Initialize clicksound
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.PlaySound(sounds[1]);
                Thread.Sleep(700);
                sound.Dispose();
                //Removes the clicksound and dispose from use (Auto GB collector will handle it)
                switch (option.ToLower())
                {
                    case "1":
                        InventoryMenuPotions(player, potions);

                        break;
                    case "2":
                        InventoryMenuItemsWeapons(player, "Item");


                        break;
                    case "3":
                        InventoryMenuItemsWeapons(player, "Weapon");

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
        //creates lists of all inventory 
        private void CreateListOfAllInventory(Player player)
        {
            weapons.Clear();
            potions.Clear();
            items.Clear();
            yourEquippableEquipment.Clear();

            foreach (var item in player.PrintAllItems())
            {
                if (item is IWeapon weapon)
                {
                    weapons.Add(weapon);
                }
                else if (item is IConsumable potion)
                {
                    potions.Add(potion);
                }
                else if (item is IItem itemX)
                {
                    items.Add(itemX);
                }
                if (item is IEquippable equipment)
                {
                    yourEquippableEquipment.Add(equipment);
                }


            }
        }















        /**************************************************
                        START OF POTIONS MENU
         **************************************************
         £££££££££££££££££££££££££££££££££££££££££££££££££££
            £££££££££££££££££££££££££££££££££££££££££££££
                £££££££££££££££££££££££££££££££££££££
                    £££££££££££££££££££££££££££££
                        £££££££££££££££££££££
                            £££££££££££££
                                £££££
                                  £
         */
        private void InventoryMenuPotions(Player player, List<IConsumable> potions)
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





                do
                {
                    int topPosition = 13;
                    int leftPosition = 28;
                    Print.ClearAllScreen(leftPosition, topPosition);







                    //Print out the potions list
                    if (potions.Count >= 1)
                    {
                        for (int i = 0; i < potions.Count; i++)
                        {
                            Console.SetCursorPosition(leftPosition, topPosition);
                            Print.YellowW($"{i + 1}. {potions[i].Name} ");
                            Console.Write($"- {potions[i].Describe()}");
                            topPosition++;
                            if (i == potions.Count - 1)
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
                        continueCode = NothingInYourInventory(ref topPosition, leftPosition);
                        break;

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

                    //Drink the potion 
                    if (successConvert && userChoice <= potions.Count && player.Health < player.MaxHealth)
                    {
                        error = true;
                        errorMsg = player.Consume(potions[userChoice - 1]);
                        potions.RemoveAt(userChoice - 1);
                        Print.PlayerStatsPrint(player);
                    }
                    //if pressed b, go back to the inventory main menu.
                    else if (!successConvert && input.ToLower() == "b")
                    {
                        continueCode = true;
                    }
                    //user entered wrong input
                    else
                    {
                        error = true;
                        errorMsg = "Wrong menu choice";

                    }
                    //if user already at max health
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


        /**************************************************
                    START OF ITEMS & WEAPONS MENU
         **************************************************
         £££££££££££££££££££££££££££££££££££££££££££££££££££
            £££££££££££££££££££££££££££££££££££££££££££££
                £££££££££££££££££££££££££££££££££££££
                    £££££££££££££££££££££££££££££
                        £££££££££££££££££££££
                            £££££££££££££
                                £££££
                                  £
         */
        private void InventoryMenuItemsWeapons(Player player, string x)
        {
            itemOrWeapon = x;
            CreateListOfAllInventory(player);

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
                Print.RedW($"---- {itemOrWeapon.ToUpper()}S ----");
                Console.CursorVisible = false;

                do
                {

                    continueCode = false;
                    int topPosition = 13;
                    int leftPosition = 28;
                    Print.ClearAllScreen(left, top);

                    Console.SetCursorPosition(29, 11);
                    Print.RedW($"---- {itemOrWeapon.ToUpper()}S ----");
                    Console.CursorVisible = false;

                    int i = 0;
                    if (yourEquippableEquipment.Count >= 1)
                    {
                        yourEquippableEquipment.RemoveAll(x => x.Type != itemOrWeapon);
                        foreach (var item in yourEquippableEquipment.Where(x => x.Type == itemOrWeapon))
                        {
                            Console.SetCursorPosition(leftPosition, topPosition);
                            Print.YellowW($"{i + 1}. {item.Name} ");
                            if (item.Equipped)
                            {
                                Print.Red("Equipped");
                            }

                            topPosition++;




                            i++;
                        }
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"B. Go back to inventory");

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
                        if (okChoice && convertedMenuChoice <= 7 || option.ToLower() == "b")
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

                                    PrintInfoAboutItemOrWeaponInTheMainWindow(yourEquippableEquipment, option, player, ref error, ref errorMsg);
                                    continueCode = false;
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
                        continueCode = NothingInYourInventory(ref topPosition, leftPosition);

                    }





                    option = default;
                } while (!continueCode);

            } while (!continueCode);
            top = 13;
            left = 28;
            Print.ClearAllScreen(left, top);
            left = 45;
        }

        //If the user has nothing in the inventory, messages ar printed.
        private bool NothingInYourInventory(ref int topPosition, int leftPosition)
        {
            bool continueCode;
            Console.SetCursorPosition(leftPosition, topPosition);
            Console.WriteLine("You have no things in your inventory. Go to the shop and buy some.");
            topPosition += 2;
            Console.SetCursorPosition(leftPosition, topPosition);
            Console.CursorVisible = false;
            Print.YellowW($"Press enter to continue");
            continueCode = true;
            Console.ReadKey();
            return continueCode;
        }

        //In a weapon or item is chosen in the inventory, a small box is appearing when item is pressed.
        //This gives info about the item/weapon and the option to equip/unequip it.
        private void PrintInfoAboutItemOrWeaponInTheMainWindow(List<IEquippable> itemList, string userchoice, Player player, ref bool error, ref string errorMsg)
        {
            bool continueCode = false;
            string option = default;
            Print.PrintHorizontalLine(28, 28);
            int topPosition = 29;
            int leftPosition = 28;
            Console.SetCursorPosition(leftPosition, topPosition);
            if (Convert.ToInt32(userchoice) <= itemList.Count)
            {
                bool equipped = itemList[Convert.ToInt32(userchoice) - 1].Equipped;
                string name = itemList[Convert.ToInt32(userchoice) - 1].Name;
                string describe = itemList[Convert.ToInt32(userchoice) - 1].Describe();

                //If it is equipped already. Print out the status
                if (equipped)
                {
                    Print.GreenW($"{name} ");
                    Print.RedW($"(Equipped) ");
                    Print.GreenW($"- {describe}");
                }
                else
                {
                    Print.GreenW($"{name} - {describe}");
                }
                topPosition++;
                Console.SetCursorPosition(leftPosition, topPosition);
                if (itemOrWeapon != "Weapon")
                {
                    if (itemList[Convert.ToInt32(userchoice) - 1] is IAmulett amulett)
                    {
                        Console.WriteLine($"Agility: {amulett.Agility}");
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"Strength: {amulett.Strength}");
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"Hp: {amulett.Hp}");
                    }
                    else if (itemList[Convert.ToInt32(userchoice) - 1] is IShoes shoes)
                    {
                        Console.WriteLine($"Agility: {shoes.Agility}");
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);

                    }
                    else if (itemList[Convert.ToInt32(userchoice) - 1] is IArmor armor)
                    {
                        Console.WriteLine($"Agility: {armor.Agility}");
                        topPosition++;
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.WriteLine($"Armor: {armor.Armor}");

                    }
                }






                topPosition = 34;
                leftPosition = 28;
                error = false;
                errorMsg = default;
                do
                {


                    if (error)
                    {
                        Console.SetCursorPosition(leftPosition, topPosition + 1);
                        error = false;
                        Console.WriteLine(errorMsg);
                        errorMsg = default;

                    }
                    //If not equipped, give the player the option to equip.
                    if (!equipped)
                    {
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.CursorVisible = true;
                        Console.Write("Would you like to equip this? y/n> ");
                        option = Console.ReadLine();
                        Console.CursorVisible = false;
                        switch (option.ToLower())
                        {
                            case "y":
                                errorMsg = player.Equip(itemList[Convert.ToInt32(userchoice) - 1], player, false);


                                error = true;
                                continueCode = true;
                                break;
                            case "n":
                                continueCode = true;
                                break;
                            default:
                                error = true;
                                errorMsg = "Valid choices y/n";
                                break;
                        }

                    }
                    //Else giv the player the option to unequip
                    else
                    {
                        Console.SetCursorPosition(leftPosition, topPosition);
                        Console.CursorVisible = true;
                        Console.Write("unequip y/n> ");
                        option = Console.ReadLine();
                        Console.CursorVisible = false;
                        switch (option.ToLower())
                        {
                            case "y":
                                player.Equip(itemList[Convert.ToInt32(userchoice) - 1], player, true);

                                continueCode = true;

                                break;
                            case "n":
                                continueCode = true;
                                break;
                            default:
                                error = true;
                                errorMsg = "Valid choices y/n";
                                break;
                        }
                    }


                } while (!continueCode);
            }
            else
            {
                error = true;
                errorMsg = "Wrong menu choice";
            }

            Print.RemoveHorizontalLineArea(28, 30);
            Print.PlayerStatsPrint(player);
        }
    }
}
