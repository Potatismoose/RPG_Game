using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class BroadSword : Weapon
    {
        public BroadSword() : base("Broad sword", 23)
        {
            Price = 160;
        }

        public override string ToString()
        {
            return $"Standard sword with {Damage} damage";
        }

        public override string Describe()
        {
            return $"Standard sword with {Damage} damage";
        }
    }
}
