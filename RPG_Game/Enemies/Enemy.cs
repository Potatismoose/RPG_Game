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

        public Enemy()
        { 
            Health = 100;
            Strength = 10;
        }

        public int Attack(Player player)
        {
            int damage = Strength;
            return damage;
        }
    }

}
