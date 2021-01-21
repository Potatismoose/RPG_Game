using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;

namespace RPG_Game.Interfaces
{
     interface IEnemy
    {
        public string Type { get; }
        public int Health { get; }
        public int Strength { get; }
        public int Gold { get; }
        public int Agility { get; }
        public bool Alive { get;}
        

        public string Attack(Player player);
        public void TakeDamage(StringBuilder textToPrint, int damage, bool lucky, int luckyDamage);
        public int DropGold();
        
    }
}
