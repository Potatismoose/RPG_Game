using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using RPG_Game.Enemies;

namespace RPG_Game.Gamer
{
    class Player
    {

        public string Name { get; }
        private int Health { get; set; }
        private int Strength { get; set; }
        private int Armor { get; set; }
        private int Gold { get; set; }
        private int Level { get; set; }
        private int Agility { get; set; }
        private int StrengthAmulett { get; set; }
        private int LuckyDamage { get; set; }


        public Player(string name)
        {
            this.Name = name;
            Health = 100;
            Strength = 10;
            Armor = 0;
            Gold = 50;
            Level = 1;
            Agility = 2;
            StrengthAmulett = 0;
            LuckyDamage = 8;

        }

        public void Attack(Enemy enemy)
        {
            Random lucky = new Random();
            if (lucky.Next(1, 101) <= 5)
            {
                Console.WriteLine($"You are feeling lucky, You might deal extra damage.");
                Thread.Sleep(300);
                enemy.TakeDamage(Strength + StrengthAmulett + LuckyDamage, true);

            }
            else 
            { 
                enemy.TakeDamage(Strength + StrengthAmulett + LuckyDamage, false); 
            }
            
        }

        public bool TakeDamage(int damage, bool enemyAttack) 
        {
            bool died = false;
            if (enemyAttack)
            {
                Random rand = new Random();
                if (rand.Next(1, 101) <= Agility)
                {
                    Console.WriteLine("You evaded the enemy attack");
                }
                else
                {
                    Health -= damage;
                }
            }
            else
            {
                Health -= damage;
            }
            if (Health <= 0)
            { 
                died = true; 
            }
            return died;
        }

        

        public Dictionary<string,int> ShowCurrentStatus()
        {
            Dictionary<string, int> playerStatus = new Dictionary<string, int> {
                {"Health", Health },
                {"Attack Strength", Strength},
                {"Armor", Armor },
                {"Gold", Gold },
                {"Level", Level },
            };

            return playerStatus;
        }

        
    }
}
