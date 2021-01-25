using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    [Serializable]
    class RustySword : Weapon
    {
        public RustySword(string name, int damage) : base(name, damage)
        {

        }
    }
}
