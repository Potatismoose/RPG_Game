using RPG_Game.Consumables;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using RPG_Game.Items;
using RPG_Game.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace RPG_Game.TheShop
{
    class Shop
    {
        int top = default(int);
        int left = default(int);
        int deleteTopHeadingsRight = 29;
        Dictionary<string, IInventoryable> itemCreator = new Dictionary<string, IInventoryable>();
        List<Weapon> weapons = new List<Weapon>();
        List<Item> items = new List<Item>();
        List<Potion> potions = new List<Potion>();
        Menu _menuObject;
        Player player;
        public Shop(Menu _menuObject, Player player)
        {
            this.player = player;
            this._menuObject = _menuObject;
        }

        //Shop main
        public void GoIn(Player player)
        {
            
            
            itemCreator.Add("Healing potion", (IInventoryable)new HealingPotion(player.MaxHealth, "Healing potion"));
            itemCreator.Add("Max healing potion", (IInventoryable)new MaxHealingPotion(player.MaxHealth));
            itemCreator.Add("Magic agility potion", (IInventoryable)new MagicAgilityPotion());
            itemCreator.Add("Fast shoes", (IInventoryable)new FastShoes());
            itemCreator.Add("Standard armor", (IInventoryable)new StandardArmor(player.Level));
            itemCreator.Add("Swift armor", (IInventoryable)new SwiftArmor(player.Level));
            itemCreator.Add("Heavy armor", (IInventoryable)new HeavyArmor(player.Level));
            itemCreator.Add("Diamond armor", (IInventoryable)new DiamondArmor(player.Level));
            itemCreator.Add("Rusty sword", (IInventoryable)new RustySword());
            itemCreator.Add("Broad sword", (IInventoryable)new BroadSword());
            itemCreator.Add("The beheader", (IInventoryable)new TheBeheader());
            itemCreator.Add("Devils blade", (IInventoryable)new DevilsBlade());
            itemCreator.Add("Dragon slayer", (IInventoryable)new DragonSlayer());


                bool continueCode = false;
                string option;
                bool error = false;
                string errorMsg = default(string);
                
                string[] inventoryOptions = new string[4] { "Potions", "Items", "Weapons", "Back to main menu" };
                do
                {

                Print.ClearAllScreen();
                //Sets the parameters for where the frame should start printing and prints the frame.
                Print.PrintSplitMenuFrame(99, 26);
                Console.SetCursorPosition(4, 11);
                Print.RedW("---- THE SHOP ----");

                Print.ClearAllScreen(deleteTopHeadingsRight, 11);
                Console.SetCursorPosition(63, 11);
                Print.YellowW(player.InventoryStatus());
                weapons.Clear();
                items.Clear();
                potions.Clear();
                
                foreach (var item in itemCreator)
                {


                    if (item.Value is Weapon)
                    {
                        weapons.Add((Weapon)item.Value);

                    }
                    else if (item.Value is Item)
                    {
                        items.Add((Item)item.Value);
                    }
                    else if (item.Value is Potion)
                    {
                        potions.Add((Potion)item.Value);
                    }

                }

                PrintWeapons(weapons);
                PrintItems(items);
                PrintPotions(potions, false);
                PrintCurrentInventory(player, potions);


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
                        StoreMenuPotions();

                        break;
                    case "2":


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

            
            //private void InventoryMenuPotions()
            //{
            //    bool continueCode = false;
            //    string option;
            //    bool error = false;
            //    string errorMsg = default(string);
            //    int saveReminder = default(int);
            //    string[] inventoryOptions = new string[4] { "Potions", "Items", "Weapons", "Back to main menu" };
            //    do
            //    {

            //        Print.ClearAllScreen();
            //        //Sets the parameters for where the frame should start printing and prints the frame.
            //        Print.PrintSplitMenuFrame(99, 26);
            //        Console.SetCursorPosition(4, 11);

            //        Print.RedW("---- INVENTORY ----");
            //        Console.SetCursorPosition(29, 11);
            //        Print.RedW("---- POTIONS ----");


            //        //Sets the parameters for where the menu should start printing.
            //        top = 13;
            //        left = 2;
            //        //prints the menu
            //        for (int i = 0; i < inventoryOptions.Length; i++)
            //        {

            //            Console.SetCursorPosition(left, top);
            //            Console.WriteLine($"{i + 1}. {inventoryOptions[i]}");
            //            top++;
            //        }

            //        Console.CursorVisible = false;
            //        List<IInventoryable> returnList = new List<IInventoryable>();
            //        List<IConsumable> listOfInventory = new List<IConsumable>();
            //        do
            //        {
            //            int topPosition = 13;
            //            int leftPosition = 28;
            //            Print.ClearAllScreen(leftPosition, topPosition);
            //            returnList.Clear();
            //            listOfInventory.Clear();

            //            returnList = player.PrintAllItems("Potion");
            //            listOfInventory = returnList.Cast<IConsumable>().ToList();

            //            if (listOfInventory.Count >= 1)
            //            {
            //                for (int i = 0; i < listOfInventory.Count; i++)
            //                {
            //                    Console.SetCursorPosition(leftPosition, topPosition);
            //                    Print.YellowW($"{i + 1}. {listOfInventory[i].Name} ");
            //                    Console.Write($"- {listOfInventory[i].Describe()}");
            //                    topPosition++;
            //                    if (i == listOfInventory.Count - 1)
            //                    {
            //                        topPosition++;
            //                        Console.SetCursorPosition(leftPosition, topPosition);
            //                        Console.WriteLine($"{i + 2}. Go back to inventory");
            //                        topPosition++;
            //                        Console.SetCursorPosition(leftPosition, topPosition);
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                Console.SetCursorPosition(leftPosition, topPosition);
            //                Console.WriteLine("You have no potions in your inventory. Go to the shop and buy some.");
            //                topPosition++;
            //                Console.SetCursorPosition(leftPosition, topPosition);
            //                Console.WriteLine($"1. Go back to inventory");
            //                topPosition++;
            //                Console.SetCursorPosition(leftPosition, topPosition);
            //            }
            //            Console.Write("Choose option> ");

            //            //Error message is printed out (if there are any)
            //            if (error)
            //            {
            //                Console.SetCursorPosition(leftPosition, topPosition + 1);
            //                Console.CursorVisible = false;
            //                Print.Red(errorMsg);
            //                errorMsg = default(string);
            //            }

            //            Console.SetCursorPosition(leftPosition + 15, topPosition);
            //            Console.CursorVisible = true;
            //            string input = Console.ReadLine();
            //            Console.CursorVisible = false;
            //            int userChoice;
            //            bool successConvert = int.TryParse(input, out userChoice);
            //            if (successConvert && userChoice <= listOfInventory.Count)
            //            {
            //                error = true;
            //                errorMsg = player.Consume(listOfInventory[userChoice - 1]);
            //                Print.PlayerStatsPrint(player);
            //            }
            //            else if (successConvert && userChoice == listOfInventory.Count + 1)
            //            {
            //                continueCode = true;
            //            }
            //            else
            //            {
            //                error = true;

            //            }

            //        } while (!continueCode);

            //    } while (!continueCode);
            //    top = 13;
            //    left = 28;
            //    Print.ClearAllScreen(left, top);
            //    left = 45;
            //}




































































            //    string[] shopOptions = new string[4] { "Potions", "Items", "Weapons", "Back to your adventure" };

            //    bool continueCode = false;
            //    string option;
            //    bool error = false;

            //    do
            //    {

            //        Print.ClearAllScreen();
            //        player.PrintCurrentPlayerStatus();

            //        top = 13;
            //        left = 45;
            //        Console.SetCursorPosition(left, top-1);
            //        Print.Red("--- The shop ---");

            //        for (int i = 0; i < shopOptions.Length; i++)
            //        {
            //            Console.SetCursorPosition(left, top);
            //            Console.WriteLine($"{i + 1}. {shopOptions[i]}");
            //            top++;
            //        }
            //        if (error)
            //        {
            //            Console.SetCursorPosition(left, top + 2);
            //            Print.Red("Wrong menu choice");
            //        }
            //        Console.SetCursorPosition(left, top + 1);
            //        Console.Write("Choose your option> ");
            //        Console.CursorVisible = true;
            //        option = Console.ReadLine();
            //        Console.CursorVisible = false;

            //        switch (option)
            //        {
            //            case "1":
            //                error = false;
            //                ShopPotions(player);
            //                break;
            //            case "2":
            //                error = false;
            //                break;
            //            case "3":

            //                break;
            //            case "4":
            //                continueCode = true;
            //                break;
            //            default:
            //                error = true;
            //                Console.WriteLine("");
            //                break;
            //        }


            //    } while (!continueCode);
            //}

            ////Shop for potions
            //private void ShopPotions(Player player)
            //{
            //    HealingPotion healing = new HealingPotion(player.MaxHealth, "Healing potion");
            //    MagicAgilityPotion magic = new MagicAgilityPotion();
            //    MaxHealingPotion maxHealing = new MaxHealingPotion(player.MaxHealth);
            //    List<IConsumable> consumable = new List<IConsumable>();
            //    consumable.Add(healing);
            //    consumable.Add(magic);
            //    consumable.Add(maxHealing);
            //    string[] shopOptions = new string[4] {healing.Name, magic.Name, maxHealing.Name,"Back to shop"};
            //    bool continueCode = false;
            //    string option;
            //    bool error = false;
            //    string errorMsg = default(string);
            //    do
            //    {


            //        Print.ClearAllScreen();

            //        player.PrintCurrentPlayer();

            //        left = 45;
            //        top = 13;
            //        Console.SetCursorPosition(left, top-1);
            //        Print.Red("--- The shop ---");

            //        int i = 0;

            //        foreach (var item in shopOptions)
            //        {
            //            Console.SetCursorPosition(left, top);
            //            Console.WriteLine($"{i + 1}. {item}");
            //            i++;
            //            top++;
            //        }






            //        if (error)
            //        {
            //            Console.SetCursorPosition(left, top + 2);
            //            if (string.IsNullOrEmpty(errorMsg))
            //            {
            //                Print.Red("Wrong menu choice");
            //            }
            //            else
            //            {
            //                Print.Red(errorMsg);
            //            }
            //            error = false;
            //        }
            //        Console.SetCursorPosition(left, top + 1);
            //        Console.Write("Choose your option> ");
            //        Console.CursorVisible = true;
            //        option = Console.ReadLine();
            //        Console.SetCursorPosition(left, top + 1);

            //        Console.CursorVisible = false;

            //        switch (option)
            //        {
            //            case "1":



            //                int whichIsLongest = default(int);
            //                foreach (var item in consumable)
            //                {
            //                    if (item.Describe().Length > whichIsLongest)
            //                    {
            //                        whichIsLongest = item.Describe().Length;
            //                    }
            //                }
            //                top += 3;
            //                Console.SetCursorPosition(left, top-1);
            //                Console.WriteLine(new string(' ',40));
            //                Console.SetCursorPosition(left, top);

            //                for (int j = 0; j < whichIsLongest+4; j++)
            //                {
            //                    Print.RedW("*");
            //                }
            //                top += 2;
            //                Console.SetCursorPosition(left + 2, top);
            //                Console.WriteLine(healing.Describe()); 
            //                top+=1;
            //                Console.SetCursorPosition(left + 2, top);
            //                Print.Yellow(healing.ToString());
            //                top += 1;
            //                Console.SetCursorPosition(left + 2, top);
            //                Print.Red("Press enter to continue or B to buy");
            //                top += 2;
            //                Console.SetCursorPosition(left, top);
            //                for (int j = 0; j < whichIsLongest + 4; j++)
            //                {
            //                    Print.RedW("*");
            //                }
            //                ConsoleKey key3;
            //                do
            //                {
            //                    var keytest = Console.ReadKey(true);
            //                    key3 = keytest.Key;
            //                    if (key3 == ConsoleKey.B)
            //                    {
            //                        if (player.Gold >= healing.Price)
            //                        {

            //                            player.PayInShop(healing.Price);
            //                            errorMsg = player.AddToBackpack(healing);
            //                            error = true;
            //                            break;
            //                        }
            //                        else
            //                        {
            //                            errorMsg = $"You can´t afford that";
            //                            error = true;
            //                        }

            //                    }
            //                } while (key3 != ConsoleKey.Enter);










            //                break;
            //            case "2":

            //                break;
            //            case "3":

            //                break;
            //            case "4":
            //                continueCode = true;
            //                break;
            //            default:
            //                error = true;
            //                Console.WriteLine("");
            //                break;
            //        }


            //    } while (!continueCode);
        }

        private void StoreMenuPotions()
        {

            Print.ClearAllScreen(deleteTopHeadingsRight, 11);
            Console.SetCursorPosition(63, 11);
            Print.YellowW(player.InventoryStatus());
            Console.SetCursorPosition(29, 11);
            Print.RedW("---- POTIONS ----");
            PrintPotions(potions, true);
            Console.ReadKey();

        }

        private void PrintCurrentInventory(Player player, List<Potion> potions)
        {
            /****************************************************************
                             INVENTORY PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            top = 13;
            left = 75;

            Console.SetCursorPosition(left, top);
            Print.Red("Current inventory");
            top++;

            
            foreach (var item in player.PrintAllItems().Where(x => x.Type =="Potion"))
            {
                Console.SetCursorPosition(left, top);
                Print.Yellow(item.Name);
                top++;
            }
        }

        private void PrintPotions(List<Potion> potions, bool extraInfo)
        {
            /****************************************************************
                             POTION PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            top = 13;
            left = 51;
            if (extraInfo)
            {
                left = 28;
                Print.ClearAllScreen(28, 13);

            }
            Console.SetCursorPosition(left, top);
            Print.RedW("Buyable Potions");
            top++;

            if (potions.Count < 1)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine("No shoppable potions");
            }
            int counter = 1;

            if (extraInfo)
            {
                
                foreach (var item in potions)
                {

                    Console.SetCursorPosition(left, top);
                    Print.YellowW($"{counter}. {item.Name}");
                    Console.Write($" +{item.TheChange}");
                    counter++;
                    top++;
                }
            }
            else 
            {
                foreach (var item in potions)
                {
                    Console.SetCursorPosition(left, top);
                    Console.Write($"{item.Name}");
                    top++;
                }
            }

        }

        private void PrintItems(List<Item> items)
        {
            /****************************************************************
                             ITEM PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            top = 23;
            left = 28;

            Console.SetCursorPosition(left, top);
            Print.Red("Buyable Items");
            top++;

            if (items.Count < 1)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine("No shoppable items");
            }
            foreach (var item in items)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(item.Name);
                top++;
            }
        }

        private void PrintWeapons(List<Weapon> weapons)
        {
            /****************************************************************
                                 WEAPON PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            top = 13;
            left = 28;
            Console.SetCursorPosition(left, top);
            Print.Red("Buyable Weapons");
            top++;

            if (weapons.Count < 1)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine("No shoppable weapons");
            }
            foreach (var item in weapons)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine(item.Name);
                top++;
            }
        }
    }
}
