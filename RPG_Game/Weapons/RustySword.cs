using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class RustySword : Weapon
    {
        public RustySword() : base("Rusty sword", 15)
        {
            Price = 90;
        }

        public override string ToString()
        {
            return $"You found this in a ditch, {Damage} damage";
        }
    }
}
