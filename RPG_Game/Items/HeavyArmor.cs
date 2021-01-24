using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class HeavyArmor : Item
    {
        public HeavyArmor(int playerLevel, string name) : base(name) { }
    }
}
