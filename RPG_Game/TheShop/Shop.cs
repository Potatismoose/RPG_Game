using RPG_Game.Consumables;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using RPG_Game.Items;
using RPG_Game.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RPG_Game.TheShop
{
    class Shop
    {
        int top = default;
        int left = default;
        int deleteTopHeadingsRight = 29;
        readonly Dictionary<string, IInventoryable> itemCreator = new Dictionary<string, IInventoryable>();
        List<IWeapon> weapons = new List<IWeapon>();
        List<IItem> items = new List<IItem>();
        List<IConsumable> potions = new List<IConsumable>();
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

            //Create a dictionary with name of item/weapon/potion and the creationstring for creating a new object
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
            itemCreator.Add("Agility amulett", (IInventoryable)new AgilityAmulett());
            itemCreator.Add("Strength amulett", (IInventoryable)new StrengthAmulett());





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
                Print.RedW("---- THE SHOP ----");

                Print.ClearAllScreen(deleteTopHeadingsRight, 11);
                Console.SetCursorPosition(63, 11);
                Print.YellowW(player.InventoryStatus());
                weapons.Clear();
                items.Clear();
                potions.Clear();

                //loop thru and add the creation strings to specific lists
                foreach (var item in itemCreator)
                {


                    if (item.Value is IWeapon)
                    {
                        weapons.Add((Weapon)item.Value);

                    }
                    else if (item.Value is IItem)
                    {
                        items.Add((Item)item.Value);
                    }
                    else if (item.Value is Potion)
                    {
                        potions.Add((Potion)item.Value);
                    }

                }

                PrintWeapons(weapons, false);
                PrintItems(items, false);
                PrintPotions(potions, false);
                PrintCurrentInventory(player, "All");


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
                top++;
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
                switch (option.ToLower())
                {
                    case "1":
                        StoreMenuPotions();

                        break;
                    case "2":
                        StoreMenuItems();

                        break;
                    case "3":
                        StoreMenuWeapons();
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

        private void StoreMenuPotions()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default;
            top = 0;
            left = 28;
            do
            {
                Print.ClearAllScreen(deleteTopHeadingsRight, 11);
                Console.SetCursorPosition(63, 11);
                Print.YellowW(player.InventoryStatus());
                Console.SetCursorPosition(29, 11);
                Print.RedW("---- POTIONS ----");
                PrintCurrentInventory(player, "Potions");
                PrintPotions(potions, true);
                Print.Yellow("B. Back to shop menu");
                top = Console.CursorTop;


                Console.SetCursorPosition(left, top);

                Console.CursorVisible = true;
                if (error)
                {
                    Console.SetCursorPosition(left, top + 1);
                    if (errorMsg.Contains("added") && !errorMsg.Contains("not been added"))
                    {
                        Print.Green(errorMsg);
                    }
                    else
                    {
                        Print.Red(errorMsg);
                    }

                    Console.SetCursorPosition(left, top);
                    error = false;
                    errorMsg = default;
                }
                Console.Write("Choose your option> ");


                option = Console.ReadLine();
                Console.CursorVisible = false;
                switch (option.ToLower())
                {
                    case "1":
                    case "2":
                    case "3":
                        bool paymentsuccess = false;
                        foreach (var item in itemCreator.Where(x => x.Key == potions[Convert.ToInt32(option) - 1].Name))
                        {
                            if (!player.IsInventoryFull())
                            {
                                paymentsuccess = player.PayInShop(potions[Convert.ToInt32(option) - 1].Price);
                                if (paymentsuccess)
                                {
                                    player.AddToBackpack(item.Value);
                                    error = true;
                                    errorMsg = $"{potions[Convert.ToInt32(option) - 1].Name} is added to your inventory";
                                    Print.PlayerStatsPrint(player);
                                }
                                else
                                {
                                    error = true;
                                    errorMsg = "You can´t afford that";
                                }
                            }
                            else
                            {
                                error = true;
                                errorMsg = "Your inventory is full and the item has not been added";
                            }
                        }


                        break;
                    case "b":
                        continueCode = true;
                        break;
                    default:
                        error = true;
                        errorMsg = "Wrong menu choice";

                        break;
                }


            } while (!continueCode);

        }

        /****************************************************************
                             STORE ITEMS 
        ****************************************************************/


        private void StoreMenuItems()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default;
            top = 0;
            left = 28;
            do
            {
                Print.ClearAllScreen(deleteTopHeadingsRight, 11);
                Console.SetCursorPosition(63, 11);
                Print.YellowW(player.InventoryStatus());
                Console.SetCursorPosition(29, 11);
                Print.RedW("---- ITEMS ----");
                left = 28;
                top = 13;
                Print.ClearAllScreen(left, top);
                Console.SetCursorPosition(left, top);

                top++;

                PrintItems(items, true);
                top++;
                Console.SetCursorPosition(left, top);
                Print.Yellow("B. Back to shop menu");
                top += 2;
                Console.SetCursorPosition(left, top);


                Console.CursorVisible = true;
                if (error)
                {
                    Console.SetCursorPosition(left, top + 1);
                    if (errorMsg.Contains("added") && !errorMsg.Contains("not been added"))
                    {
                        Print.Green(errorMsg);
                    }
                    else
                    {
                        Print.Red(errorMsg);
                    }

                    Console.SetCursorPosition(left, top);
                    error = false;
                    errorMsg = default;
                }
                Console.Write("Choose your option> ");


                option = Console.ReadLine();
                Console.CursorVisible = false;
                switch (option.ToLower())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":


                        bool paymentsuccess = false;
                        foreach (var item in itemCreator.Where(x => x.Key == items[Convert.ToInt32(option) - 1].Name))
                        {
                            if (!player.IsInventoryFull())
                            {
                                bool owned = OwnedAllready((IEquippable)items[Convert.ToInt32(option) - 1]);
                                if (owned)
                                {
                                    error = true;
                                    errorMsg = $"You already own {items[Convert.ToInt32(option) - 1].Name}";
                                }
                                else
                                {
                                    paymentsuccess = player.PayInShop(items[Convert.ToInt32(option) - 1].Price);
                                }
                                if (paymentsuccess)
                                {
                                    player.AddToBackpack(item.Value);
                                    error = true;
                                    errorMsg = $"{items[Convert.ToInt32(option) - 1].Name} is added to your inventory";
                                    Print.PlayerStatsPrint(player);
                                }
                                else if (!paymentsuccess && !owned)
                                {
                                    error = true;
                                    errorMsg = "You can´t afford that";
                                }
                            }
                            else
                            {
                                error = true;
                                errorMsg = "Your inventory is full and the item has not been added";
                            }
                        }


                        break;
                    case "b":
                        continueCode = true;
                        break;
                    default:
                        error = true;
                        errorMsg = "Wrong menu choice";

                        break;
                }


            } while (!continueCode);

        }




        /****************************************************************
                             STORE WEAPONS 
        ****************************************************************/


        private void StoreMenuWeapons()
        {
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default;
            top = 0;
            left = 28;
            do
            {
                Print.ClearAllScreen(deleteTopHeadingsRight, 11);
                Console.SetCursorPosition(63, 11);
                Print.YellowW(player.InventoryStatus());
                Console.SetCursorPosition(29, 11);
                Print.RedW("---- WEAPONS ----");
                left = 28;
                top = 13;
                Print.ClearAllScreen(left, top);
                Console.SetCursorPosition(left, top);

                top++;

                PrintWeapons(weapons, true);
                top++;
                Console.SetCursorPosition(left, top);
                Print.Yellow("B. Back to shop menu");
                top += 2;
                Console.SetCursorPosition(left, top);


                Console.CursorVisible = true;
                if (error)
                {
                    Console.SetCursorPosition(left, top + 1);
                    if (errorMsg.Contains("added") && !errorMsg.Contains("not been added"))
                    {
                        Print.Green(errorMsg);
                    }
                    else
                    {
                        Print.Red(errorMsg);
                    }

                    Console.SetCursorPosition(left, top);
                    error = false;
                    errorMsg = default;
                }
                Console.Write("Choose your option> ");


                option = Console.ReadLine();
                Console.CursorVisible = false;
                switch (option.ToLower())
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":


                        bool paymentsuccess = false;
                        foreach (var item in itemCreator.Where(x => x.Key == weapons[Convert.ToInt32(option) - 1].Name))
                        {
                            if (!player.IsInventoryFull())
                            {
                                bool owned = OwnedAllready((IEquippable)weapons[Convert.ToInt32(option) - 1]);
                                if (owned)
                                {
                                    error = true;
                                    errorMsg = $"You already own {weapons[Convert.ToInt32(option) - 1].Name}";
                                }
                                else
                                {
                                    paymentsuccess = player.PayInShop(weapons[Convert.ToInt32(option) - 1].Price);
                                }
                                if (paymentsuccess)
                                {
                                    player.AddToBackpack(item.Value);
                                    error = true;
                                    errorMsg = $"{weapons[Convert.ToInt32(option) - 1].Name} is added to your inventory";
                                    Print.PlayerStatsPrint(player);
                                }
                                else if (!paymentsuccess && !owned)
                                {
                                    error = true;
                                    errorMsg = "You can´t afford that";
                                }
                            }
                            else
                            {
                                error = true;
                                errorMsg = "Your inventory is full and the item has not been added";
                            }
                        }


                        break;
                    case "b":
                        continueCode = true;
                        break;
                    default:
                        error = true;
                        errorMsg = "Wrong menu choice";

                        break;
                }


            } while (!continueCode);

        }








        private void PrintCurrentInventory(Player player, string whatToPrint)
        {
            /****************************************************************
                             INVENTORY PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            top = 13;
            left = 75;



            if (whatToPrint == "Potions")
            {
                Console.SetCursorPosition(left, top);
                Print.Red("Your owned potions");
                top++;
                foreach (var item in player.PrintAllItems().Where(x => x.Type == "Potion"))
                {
                    Console.SetCursorPosition(left, top);
                    Print.Yellow(item.Name);
                    top++;
                }
            }
            else if (whatToPrint == "Weapons")

            {
                Console.SetCursorPosition(left, top);
                Print.Red("Your owned weapons");
                top++;
                foreach (var item in player.PrintAllItems().Where(x => x.Type == "Weapon"))
                {
                    Console.SetCursorPosition(left, top);
                    Print.Yellow(item.Name);
                    top++;
                }
            }

            else if (whatToPrint == "Items")

            {

                Console.SetCursorPosition(left, top);
                Print.Red("Your owned items");
                top++;
                foreach (var item in player.PrintAllItems().Where(x => x.Type == "Item"))
                {
                    Console.SetCursorPosition(left, top);
                    Print.Yellow(item.Name);
                    top++;
                }
            }
            else if (whatToPrint == "All")

            {
                Console.SetCursorPosition(left, top);
                Print.Red("Your owned items");
                top++;
                foreach (var item in player.PrintAllItems())
                {
                    Console.SetCursorPosition(left, top);
                    Print.Yellow(item.Name);
                    top++;
                }
            }

        }

        private void PrintPotions(List<IConsumable> potions, bool extraInfo)
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
                    Console.Write($"-{item}");
                    counter++;
                    top++;
                }
                top++;
                Console.SetCursorPosition(left, top);
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

        private void PrintItems(List<IItem> items, bool extra)
        {
            /****************************************************************
                             ITEM PRINTING (SUMMARY OVER INVENTORY) 
            ****************************************************************/
            if (extra)
            {
                top = 13;
                left = 28;
            }
            else
            {
                top = 23;
                left = 28;
            }


            Console.SetCursorPosition(left, top);
            Print.Red("Buyable Items");
            top++;

            if (items.Count < 1)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine("No shoppable items");
            }


            for (int i = 0; i < items.Count; i++)
            {
                Console.SetCursorPosition(left, top);
                if (extra)
                {
                    if (items[i] is IAmulett tempAmulett)
                    {
                        Print.YellowW($"{i + 1}. {tempAmulett.Name} - {tempAmulett.Price} gold, {tempAmulett.Agility} agility, {tempAmulett.Strength} Strength,  ");
                        foreach (var item in player.PrintAllItems().Where(x => x.Type == "Item" && x.Name == tempAmulett.Name))
                        {

                            Print.Green(" - Owned");

                        }
                    }
                    else if (items[i] is IArmor tempArmor)
                    {
                        Print.YellowW($"{i + 1}. {tempArmor.Name} - {tempArmor.Price} gold, {tempArmor.Agility} agility, {tempArmor.Armor} Armor,  ");
                        foreach (var item in player.PrintAllItems().Where(x => x.Type == "Item" && x.Name == tempArmor.Name))
                        {

                            Print.Green(" - Owned");

                        }
                    }

                    else if (items[i] is IShoes tempShoes)
                    {
                        Print.YellowW($"{i + 1}. {tempShoes.Name} - {tempShoes.Price} gold, {tempShoes.Agility} agility");
                        foreach (var item in player.PrintAllItems().Where(x => x.Type == "Item" && x.Name == tempShoes.Name))
                        {

                            Print.Green(" - Owned");

                        }
                    }





                }
                else
                {
                    Console.WriteLine($"{items[i].Name}");
                }
                top++;
            }



        }

        private void PrintWeapons(List<IWeapon> weapons, bool extra)
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
                Console.WriteLine("No shoppable Weapons");
            }


            for (int i = 0; i < weapons.Count; i++)
            {
                Console.SetCursorPosition(left, top);
                if (extra)
                {
                    Print.YellowW($"{i + 1}. {weapons[i].Name} - {weapons[i].Price} gold, Attack strength: {weapons[i].Damage}");
                    foreach (var item in player.PrintAllItems().Where(x => x.Type == "Weapon" && x.Name == weapons[i].Name))
                    {

                        Print.Green(" - Owned");

                    }



                }
                else
                {
                    Console.WriteLine($"{weapons[i].Name}");
                }
                top++;
            }
        }


        private bool OwnedAllready(IEquippable item)
        {
            bool owned = false;
            foreach (var equippableItem in player.PrintAllItems().Where(x => x.Name == item.Name))
            {
                owned = true;
            }
            return owned;
        }









    }
}
