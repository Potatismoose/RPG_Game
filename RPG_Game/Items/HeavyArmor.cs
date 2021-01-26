using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class HeavyArmor : Item, IArmor
    {
        public HeavyArmor(int playerLevel) : base("Heavy armor") 
        { 
        
        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }
    }
}
