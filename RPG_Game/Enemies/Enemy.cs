using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;
namespace RPG_Game.Enemies
{
    public abstract class Enemy : IEnemy
    {
        public string Type { get; private set; }
        public int Health { get; private set; }
        public int Strength { get; private set; }
        public int Gold { get; private set; }
        public int Agility { get; private set; }
        public bool Alive { get; private set; }


        public Enemy()
        { 
            Health = 100;
            Strength = 7;
            Agility = 2;
            Alive = true;
            Random rand = new Random();
            Gold = rand.Next(10, 50);

        }

        public virtual void Attack(Player player)
        {

            int damage = Strength;
            player.TakeDamage(damage, true);
            
        }

        public virtual string TakeDamage(int damage, bool lucky)
        {
            
                Random rand = new Random();
            if (rand.Next(1, 101) <= Agility)
            {
                return "The enemy evaded your attack.";
            }
            else
            {
                Health -= damage;
                if (Health <= 0)
                {
                    Alive = false;
                }
            }
            if (lucky)
            {
                return $"CRITICAL HIT! You dealt {damage} damage";
            }
            else
            {
                return $"You dealt {damage} damage to the monster.";
            }
           
        }

        public int DropGold()
        {
            return Gold;
        }

        
    }

}
