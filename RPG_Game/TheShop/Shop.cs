using RPG_Game.Consumables;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RPG_Game.TheShop
{
    class Shop
    {
        int top = default(int);
        int left = default(int);


        //Shop main
        public void GoIn(Player player)
        {
            string[] shopOptions = new string[4] { "Potions", "Items", "Weapons", "Back to your adventure" };
            //PrintMainBorder(shopOptions);
            //Console.ReadKey();
            bool continueCode = false;
            string option;
            bool error = false;

            do
            {

                Print.ClearAllScreen();
                player.PrintCurrentPlayerStatus();

                top = 13;
                left = 45;
                Console.SetCursorPosition(left, top-1);
                Print.Red("--- The shop ---");

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
                Console.CursorVisible = true;
                option = Console.ReadLine();
                Console.CursorVisible = false;

                switch (option)
                {
                    case "1":
                        error = false;
                        ShopPotions(player);
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

        //Shop for potions
        private void ShopPotions(Player player)
        {
            HealingPotion healing = new HealingPotion(player.MaxHealth, "Healing potion");
            MagicAgilityPotion magic = new MagicAgilityPotion("Magic agility potion");
            MaxHealingPotion maxHealing = new MaxHealingPotion(player.MaxHealth, "Max healing potion");
            List<IConsumable> consumable = new List<IConsumable>();
            consumable.Add(healing);
            consumable.Add(magic);
            consumable.Add(maxHealing);
            string[] shopOptions = new string[4] {healing.Name, magic.Name, maxHealing.Name,"Back to shop"};
            bool continueCode = false;
            string option;
            bool error = false;
            string errorMsg = default(string);
            do
            {
                foreach (var item in consumable)
                {
                    Console.WriteLine(item.Name);
                }

                Print.ClearAllScreen();
               
                player.PrintCurrentPlayerStatus();

                left = 45;
                top = 13;
                Console.SetCursorPosition(left, top-1);
                Print.Red("--- The shop ---");
                
                int i = 0;
                
                foreach (var item in shopOptions)
                {
                    Console.SetCursorPosition(left, top);
                    Console.WriteLine($"{i + 1}. {item}");
                    i++;
                    top++;
                }



                
                

                if (error)
                {
                    Console.SetCursorPosition(left, top + 2);
                    if (string.IsNullOrEmpty(errorMsg))
                    {
                        Print.Red("Wrong menu choice");
                    }
                    else
                    {
                        Print.Red(errorMsg);
                    }
                    error = false;
                }
                Console.SetCursorPosition(left, top + 1);
                Console.Write("Choose your option> ");
                Console.CursorVisible = true;
                option = Console.ReadLine();
                Console.CursorVisible = false;

                switch (option)
                {
                    case "1":

                        

                        int whichIsLongest = default(int);
                        foreach (var item in consumable)
                        {
                            if (item.Describe().Length > whichIsLongest)
                            {
                                whichIsLongest = item.Describe().Length;
                            }
                        }
                        top += 3;
                        Console.SetCursorPosition(left, top-1);
                        Console.WriteLine(new string(' ',40));
                        Console.SetCursorPosition(left, top);
                        
                        for (int j = 0; j < whichIsLongest+4; j++)
                        {
                            Print.RedW("*");
                        }
                        top += 2;
                        Console.SetCursorPosition(left + 2, top);
                        Console.WriteLine(healing.Describe()); 
                        top+=1;
                        Console.SetCursorPosition(left + 2, top);
                        Print.Yellow(healing.ToString());
                        top += 1;
                        Console.SetCursorPosition(left + 2, top);
                        Print.Red("Press enter to continue or B to buy");
                        top += 2;
                        Console.SetCursorPosition(left, top);
                        for (int j = 0; j < whichIsLongest + 4; j++)
                        {
                            Print.RedW("*");
                        }
                        ConsoleKey key3;
                        do
                        {
                            var keytest = Console.ReadKey(true);
                            key3 = keytest.Key;
                            if (key3 == ConsoleKey.B)
                            {
                                if (player.Gold >= healing.Price)
                                {
                                    
                                    player.PayInShop(healing.Price);
                                    errorMsg = player.AddToBackpack(healing);
                                    error = true;
                                    break;
                                }
                                else
                                {
                                    errorMsg = $"You can´t afford that";
                                    error = true;
                                }

                            }
                        } while (key3 != ConsoleKey.Enter);










                        break;
                    case "2":

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
