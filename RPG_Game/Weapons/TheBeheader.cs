using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class TheBeheader : Weapon
    {
        public TheBeheader() : base("The beheader", 35)
        {
            Price = 200;
            
        }
        public override string ToString()
        {
            return $"Sharp as a knife with orc steel, {Damage} damage";
        }

        public override string Describe()
        {
            return $"Sharp as a knife with orc steel, {Damage} damage";
        }
    }
}
