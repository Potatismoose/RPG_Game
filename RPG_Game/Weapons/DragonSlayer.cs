using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class DragonSlayer : Weapon
    {
        public DragonSlayer() : base("Dragon slayer", 85)
        {
            Price = 450;
        }

        public override string ToString()
        {
            return $"Cuts thru dragon skin, {Damage} damage";
        }

        public override string Describe()
        {
            return $"Cuts thru dragon skin, {Damage} damage";
        }
    }
}
