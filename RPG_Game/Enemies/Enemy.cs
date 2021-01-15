using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;
namespace RPG_Game.Enemies
{
    class Enemy
    {
        private string Type { get; set; }
        private int Health { get; set; }
        private int Strength { get; set; }
        private int Gold { get; set; }
        private int Agility { get; set; }


        public Enemy()
        { 
            Health = 100;
            Strength = 10;
            Agility = 2;
        }

        public int Attack(Player player)
        {
            int damage = Strength;
            return damage;
        }

        public void TakeDamage(int damage, bool lucky)
        {
            
                Random rand = new Random();
                if (rand.Next(1, 101) <= Agility)
                {
                    Console.WriteLine("The enemy evaded your attack.");
                }
                else
                {
                    Health -= damage;
                }
            if (lucky)
            {
                Console.WriteLine($"CRITICAL HIT! You dealt {damage} damage");
            }
            else
            {
                Console.WriteLine($"You dealt {damage} damage to the monster.");
            }
           
        }
    }

}
