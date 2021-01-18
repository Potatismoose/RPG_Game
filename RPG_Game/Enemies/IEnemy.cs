using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;

namespace RPG_Game.Enemies
{
    public interface IEnemy
    {
        public string Type { get; }
        public int Health { get; }
        public int Strength { get; }
        public int Gold { get; }
        public int Agility { get; }
        public bool Alive { get;}
        

        public void Attack(Player player);
        public string TakeDamage(int damage, bool lucky);
        public int DropGold();
        
    }
}
