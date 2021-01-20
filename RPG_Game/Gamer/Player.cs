using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using RPG_Game.Enemies;

namespace RPG_Game.Gamer
{   [Serializable]
    class Player : IPlayer
    {

        public string Name { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Strength { get; private set; }
        public int Armor { get; private set; }
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int Agility { get; private set; }
        public int StrengthAmulett { get; private set; }
        public int LuckyDamage { get; private set; }
        public bool Alive { get; private set; }
        public int Xp { get; private set; }
        public int Threshold { get; private set; }
        public int NextLevel {
            get
            {
                int level = 0;
                if (Level == 1)
                {
                    level = (((2 * Level) - Level) * Threshold) / 2;
                }
                else {
                     level = (((Level * Level) - Level) * Threshold) / 2;
                }

                return level;
            }
        }

        public Player(string name)
        {
            Name = name;
            
            Armor = 0;
            Gold = 50;
            Level = 1;
            Threshold = 60;
            Agility = 15;
            StrengthAmulett = 0;
            LuckyDamage = 7;
            Alive = true;
            Xp = 29;
            

            if (name == "Robin" || name == "robin")
            {
                Health = 10000;
                Strength = 200;
                Armor = 10;
                MaxHealth = 10000;
            }
            else
            {
                Random rand = new Random();
                    
                Health = 10;
                Strength = rand.Next(10,21);
                MaxHealth = 100;
                Armor = 0;
            }

        }

        public string Attack(Enemy enemy)
        {
            StringBuilder textToReturn = new StringBuilder();
            Random lucky = new Random();
            if (lucky.Next(1, 101) <= 5)
            {
                textToReturn.Append($"You are feeling lucky, You might deal extra damage.");
                Thread.Sleep(300);
                enemy.TakeDamage(textToReturn, Strength + StrengthAmulett + LuckyDamage, true, LuckyDamage);

                return textToReturn.ToString();

            }
            else 
            {
                enemy.TakeDamage(textToReturn, Strength + StrengthAmulett + LuckyDamage, false, LuckyDamage);
                return textToReturn.ToString();
            }
            
        }

        public string TakeDamage(int damage, bool enemyAttack) 
        {
            bool died = false;
            if (enemyAttack)
            {
                Random rand = new Random();
                if (rand.Next(1, 101) <= Agility)
                {
                    return "You evaded the enemy attack";
                }
                else
                {
                    if ((Health - (damage - Armor)) > MaxHealth)
                    {
                        Health = MaxHealth;
                    }
                    else
                    {
                        Health -= damage - Armor;
                    }
                    
                    
                    if (Health <= 0)
                    {
                        Alive = false;
                        Health = 0;
                        return $"The enemy attacked you, dealing {damage-Armor} damage\nGAME OVER! You died. PRESS ENTER TO CONTINUE.";
                    }
                    else
                    {
                        if (damage - Armor < 0)
                        {
                            return $"The enemy attacked you, dealing 0 damage\nPRESS ENTER TO ATTACK AGAIN!!!";
                        }
                        else {
                            return $"The enemy attacked you, dealing {damage - Armor} damage\nPRESS ENTER TO ATTACK AGAIN!!!";
                        }
                        
                    }
                }
            }
            else
            {
                Health -= damage;
                return $"The snake bit you, dealing {damage} damage";
            }
            
            
        }
        public int TakeGold(int gold)
        {
            Gold += gold;
            return gold;
        }

        public int TakeXp(int xp, Menu _menuObject)
        {
            calculateXP(xp, _menuObject);
            return xp;
        }



        private Dictionary<string,int> ShowCurrentStatus()
        {
            Dictionary<string, int> playerStatus = new Dictionary<string, int> {
                {"Health", Health },
                {"MaxHealth", MaxHealth },
                {"Attack Strength", Strength+StrengthAmulett},
                {"Armor", Armor },
                {"Gold", Gold },
                {"Level", Level },
                {"Xp", Xp },
                {"Next Lvl", NextLevel }
            };

            return playerStatus;
        }

        private int calculateXP(int xp, Menu _menuObject)
        {
            
            Xp += xp;
            if (Xp < NextLevel)
            {
                return Xp;
            }
            else
            {
                Xp = Xp - NextLevel;
                Level += 1;
                
                

                return Xp;
            }
        }

        public void PrintCurrentPlayerStatus()
        {
            //Printing out the border around the player status
            int top = 1;
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

            top = 2;
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

            top = 3;
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
                    healthLength += item.Key.Length+2;
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


    }
}
