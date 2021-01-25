using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class DragonSlayer : Weapon
    {
        public DragonSlayer(string name, int damage) : base(name, damage)
        {

        }
    }
}
