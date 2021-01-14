using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;
using RPG_Game.Enemies;

namespace RPG_Game.Gamer
{
    class Player
    {
        private string Name { get; set; }
        private int Health { get; set; }
        private int Strength { get; set; }
        private int Armor { get; set; }
        private int Gold { get; set; }

        public Player(string name)
        {
            this.Name = name;
            Health = 100;
            Strength = 10;
            Armor = 2;

        }

        public void Attack(Enemy enemy)
        {

            
        }

        
    }
}
