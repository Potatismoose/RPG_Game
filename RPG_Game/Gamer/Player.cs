﻿using RPG_Game.Consumables;
using RPG_Game.Enemies;
using RPG_Game.Interfaces;
using RPG_Game.Items;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPG_Game.Gamer
{
    [Serializable]
    class Player
    {
        //Reserving a backpack for the player
        readonly Backpack backpack;

        public string Name { get; private set; }
        public int Health { get; set; }
        public int MaxHealth { get; private set; }
        public int Strength { get; set; }
        public int Armor { get; set; }
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int KilledCreatures { get; private set; }

        private int agility;
        public int Agility
        {
            get { return agility; }
            set { agility = value; }
        }
        public string error = default;
        private bool temporarySet;

        //Used by the agility potion
        private int tempAgility;
        public int TempAgility
        {
            get { return tempAgility; }
            set
            {


                if (temporarySet && value > 0)
                {
                    error = "Have you been drinking?";
                }
                if (value > 0 && !temporarySet)
                {
                    tempAgility = value;
                    temporarySet = true;
                    Agility += tempAgility;
                }
                else if (value <= 0 && temporarySet)
                {
                    Agility -= tempAgility;
                    tempAgility = value;
                    temporarySet = false;
                    error = default;
                }
                else
                {
                    tempAgility = 0;
                }

            }
        }

        //Temporary saving item/weapon equipped effect to be removed when unequipped
        public int StrengthAmulett { get; set; }
        public int HealthAmulett { get; set; }
        public int AgilityAmulett { get; set; }
        public int StrengthWeapon { get; set; }
        public int AgilityWeapon { get; set; }
        public int AgilityShoe { get; set; }
        public int AgilityArmor { get; set; }
        public int ArmorArmor { get; set; }


        public int LuckyDamage { get; private set; }
        public bool Alive { get; private set; }
        public int Xp { get; private set; }
        public int Threshold { get; private set; }
        //Calculation for next level XP
        public int NextLevel
        {
            get
            {
                int level = 0;
                if (Level == 1)
                {
                    level = (((2 * Level) - Level) * Threshold) / 2;
                }
                else
                {
                    if (Level == 10)
                    {
                        Level = 1;
                        Xp = 0;
                    }
                    level = (((Level * Level) - Level) * Threshold) / 2;
                }

                return level;
            }
        }





        //Constructor for player
        public Player(string name)
        {
            //Creating a backpack for the player that contains 10 slots
            backpack = new Backpack(15);
            Name = name;

            Armor = 0;
            Gold = 50;
            Level = 1;
            Threshold = 60;
            Agility = 5;




            Alive = true;
            Xp = 0;

            //God mode for Robin
            if (name.ToLower() == "robin")
            {

                Health = 9000;
                Strength = 500;
                Gold = 10000;
                Armor = 100;
                MaxHealth = Health;
                LuckyDamage = (int)Math.Round((double)Strength * 0.2);


            }
            //If playername is robinlvl9, you can fight the end boss.
            else if (name.ToLower() == "robinlvl9")
            {

                Health = 9000;
                Strength = 100;
                Gold = 10000;
                Armor = 100;
                Level = 9;
                Xp = 2050;
                KilledCreatures = 9999;
                MaxHealth = Health;
                LuckyDamage = (int)Math.Round((double)Strength * 0.2);


            }
            //Semi god mode
            else if (name.ToLower() == "benny")
            {

                Health = 100;
                Gold = 10000;
                Strength = 5;
                Armor = 0;
                MaxHealth = Health;
                LuckyDamage = (int)Math.Round((double)Strength * 0.2);




            }
            //Standard settings
            else
            {
                Health = 100;
                Strength = 5;
                MaxHealth = Health;
                Armor = 0;

                //Lucky damage is 20% extra of strength
                LuckyDamage = (int)Math.Round((double)Strength * 0.2);
            }


        }



        //When potion is consumed and HP is gonna be restored
        public void RestoreHp(int healthToRestore)
        {
            if (healthToRestore + Health > MaxHealth)
            {
                Health = MaxHealth;
            }
            else
                Health += healthToRestore;
        }
        //Method for attacking
        public string Attack(Enemy enemy)
        {
            StringBuilder textToReturn = new StringBuilder();
            Random lucky = new Random();
            //If you are lucky (20% chance) you can get a critical hit (+20% on attack damage).
            //That would be if the enemy does not evade or shield themself.
            if (lucky.Next(1, 101) <= 20)
            {
                textToReturn.Append($"You are feeling lucky, You might deal extra damage.");
                Thread.Sleep(300);
                enemy.TakeDamage(textToReturn, Strength, true, LuckyDamage);

                return textToReturn.ToString();

            }
            else
            {
                enemy.TakeDamage(textToReturn, Strength, false, LuckyDamage);
                return textToReturn.ToString();
            }

        }
        //Enemy takes damage from snake or enemy
        public string TakeDamage(int damage, bool enemyAttack)
        {

            if (enemyAttack)
            {
                //Randomness to avoid getting hit
                Random rand = new Random();
                if (rand.Next(1, 101) <= Agility)
                {
                    return "You evaded the enemy attack";
                }
                else
                {
                    if ((Health - (damage - Armor)) > Health)
                    {
                        //Do nothing
                    }
                    else
                    {

                        Health -= damage - Armor;
                    }

                    //Player dies of his/hers injurys
                    if (Health <= 0)
                    {
                        Alive = false;
                        Health = 0;
                        return $"The enemy attacked you, dealing {damage - Armor} damage\nGAME OVER! You died. PRESS ENTER TO CONTINUE.";
                    }
                    else
                    {
                        //If damage-armor is less then 0, Print out that enemy did 0 damage
                        if (damage - Armor < 0)
                        {
                            return $"The enemy attacked you, dealing 0 damage\nPRESS ENTER TO ATTACK AGAIN!!!";
                        }
                        //Else Print out that enemy did X damage
                        else
                        {
                            return $"The enemy attacked you, dealing {damage - Armor} damage\nPRESS ENTER TO ATTACK AGAIN!!!";
                        }

                    }
                }
            }
            else
            {
                Health -= damage;
                if (Health <= 0)
                {
                    Alive = false;
                }
                return $"The snake bit you, dealing {damage} damage";

            }


        }
        //Loot gold (or get gold when selling item...not implemented yet)
        public int TakeGold(int gold)
        {
            Gold += gold;
            return gold;
        }
        //Get XP after fight
        public int TakeXp(int xp)
        {
            return CalculateXP(xp);
        }
        // Returns a dictionary with player vitals and stats
        private Dictionary<string, int> ShowCurrentStatus()
        {
            Dictionary<string, int> playerStatus = new Dictionary<string, int> {
                {"Health", Health },
                {"MaxHealth", MaxHealth },
                {"Attack Strength", Strength},
                {"Armor", Armor },
                {"Agility", Agility },
                {"Gold", Gold },
                {"Level", Level },
                {"Xp", Xp },
                {"Next Lvl", NextLevel }
            };

            return playerStatus;
        }
        //Calculates xp for next level
        private int CalculateXP(int xpEnemy)
        {

            Xp += xpEnemy;
            if (Xp < NextLevel)
            {
                return Xp;
            }
            else
            {
                Xp = Xp - NextLevel;
                Level += 1;
                MaxHealth = (int)Math.Round((double)MaxHealth * 1.15);
                LevelUpBonus();
                return xpEnemy;
            }
        }
        //get a gold level up bonus
        private void LevelUpBonus()
        {
            Random rand = new Random();

            Gold += rand.Next(20, 31) * Level;

        }
        //Top right corner stats/vital print
        public void PrintCurrentPlayerStatus()
        {
            //Printing out the border around the player status
            int top = 0;
            int left = 105;
            var status = ShowCurrentStatus();

            StringBuilder str = new StringBuilder();
            str.Append("*");
            str.Append(' ', 25);
            str.Append("*");
            int extraRows = 2;

            for (int i = 0; i < status.Count + extraRows - 1; i++)
            {
                if (i == 0 || i == status.Count + extraRows - 2)
                {
                    Console.SetCursorPosition(left, top);
                    Print.Red("***************************");
                }
                else
                {
                    Console.SetCursorPosition(left, top);
                    Print.Red(str.ToString());
                }
                top++;
            }

            top = 1;
            left = 107;
            Console.SetCursorPosition(left, top);
            if (Name.ToLower() == "robin")
            {
                Print.YellowW("Name: ");
                Console.WriteLine($"{Name}, the god");
            }
            else
            {
                Print.YellowW("Name: ");
                Console.WriteLine(Name);
            }

            top = 2;
            left = 107;
            int healthLength = 0;
            int xpLength = 0;
            //Printing out the status of the player
            foreach (var item in status)
            {
                if (item.Key == "Health")
                {
                    string temp = item.Value.ToString();
                    healthLength = temp.Length;
                    healthLength += item.Key.Length + 2;
                    Console.SetCursorPosition(left, top);
                    Print.YellowW($"{item.Key}: ");
                    Console.WriteLine($"{item.Value}");

                }
                else if (item.Key == "MaxHealth")
                {
                    Console.SetCursorPosition(left + healthLength, top);
                    Console.WriteLine($"/{item.Value}");
                    top++;
                }
                else if (item.Key == "Xp")
                {
                    string temp = item.Value.ToString();
                    xpLength = temp.Length;
                    xpLength += item.Key.Length + 2;
                    Console.SetCursorPosition(left, top);
                    Print.YellowW($"{item.Key}: ");
                    Console.WriteLine($"{item.Value}");


                }
                else if (item.Key == "Next Lvl")
                {
                    Console.SetCursorPosition(left + xpLength, top);
                    Console.WriteLine($"/{item.Value}");
                    top++;
                }
                else
                {

                    Console.SetCursorPosition(left, top);
                    Print.YellowW($"{item.Key}: ");
                    Console.WriteLine(item.Value);
                    top++;
                }
            }
        }
        //Add item to backpack
        public string AddToBackpack(IInventoryable item)
        {
            return backpack.AddToInventory(item);
        }
        //Remove item from backpack
        public string RemoveFromBackpack(IInventoryable item)
        {
            return backpack.RemoveFromBackpack(item);
        }
        //Print all items
        public List<IInventoryable> PrintAllItems()
        {
            return backpack.PrintAllItems();
        }
        public List<IConsumable> PrintAllItems(int noll)
        {
            return backpack.PrintAllItems(noll);

        }
        //Show space in backpack
        public int ShowSpaceInBackpack()
        {
            return backpack.ShowSpace();
        }
        //Pay when you buy something in shop
        public bool PayInShop(int price)
        {
            if (Gold - price >= 0)
            {
                Gold -= price;
                return true;
            }
            else
                return false;
        }
        //Consume a potion
        public string Consume(IConsumable potion)
        {
            MagicAgilityPotion map;
            MaxHealingPotion mhp;
            HealingPotion hp;
            int number = 0;
            switch (potion.Name)
            {
                case "Healing potion":
                    hp = (HealingPotion)potion;
                    number = hp.Consume();
                    RestoreHp(number);
                    RemoveFromBackpack((IInventoryable)potion);
                    return $"Hp restored by {number}.";

                case "Max healing potion":
                    mhp = (MaxHealingPotion)potion;
                    number = (mhp.Consume());
                    RestoreHp(number);
                    RemoveFromBackpack((IInventoryable)potion);
                    return $"Hp restored to {MaxHealth}.";

                case "Magic agility potion":
                    map = (MagicAgilityPotion)potion;
                    number = map.Consume();
                    SetAgilityTempUp(number);
                    RemoveFromBackpack((IInventoryable)potion);
                    return $"Temporary agility up by {number}";


            }
            return "Something went wrong";
        }
        //Equip a item/weapon
        public string Equip(IEquippable thing, Player player, bool remove)
        {
            return backpack.Equip(thing, player, remove);
        }
        //When magic agility potion used, set temporary agility
        public string SetAgilityTempUp(int tempUp)
        {
            TempAgility = tempUp;
            return error;
        }

        public string InventoryStatus()
        {
            return backpack.InventoryStatus();
        }
        public string ShortInfoAboutInventoryStatus()
        {

            return backpack.ShortInfoAboutInventoryStatus();
        }
        //check if inv  is full
        public bool IsInventoryFull()
        {
            return backpack.IsInventoryFull();
        }

        //Vital change after changing equipment.
        public void MakeVitalChangeAfterEquip(string type)
        {
            if (type == "Amulett")
            {
                Agility += AgilityAmulett;
                Strength += StrengthAmulett;
                Health += HealthAmulett;

                if (AgilityAmulett < 0)
                {
                    AgilityAmulett = 0;
                }
                if (StrengthAmulett < 0)
                {
                    AgilityAmulett = 0;
                }
                if (HealthAmulett < 0)
                {
                    HealthAmulett = 0;
                }
            }
            else if (type == "Weapon")
            {

                Strength += StrengthWeapon;
                Agility += AgilityWeapon;

                if (StrengthWeapon < 0)
                {
                    StrengthWeapon = 0;
                }
                if (AgilityWeapon < 0)
                {
                    AgilityWeapon = 0;
                }

            }

            else if (type == "Shoe")
            {

                Agility += AgilityShoe;

                if (AgilityShoe < 0)
                {
                    AgilityShoe = 0;
                }

            }

            else if (type == "Armor")
            {

                Armor += ArmorArmor;
                Agility += AgilityArmor;


                if (AgilityArmor < 0)
                {
                    AgilityArmor = 0;
                }
                if (ArmorArmor < 0)
                {
                    ArmorArmor = 0;
                }

            }

            LuckyDamage = (int)Math.Round((double)Strength * 0.2);
        }

        public void KilledCreature()
        {
            KilledCreatures += 1;
        }
    }
}
