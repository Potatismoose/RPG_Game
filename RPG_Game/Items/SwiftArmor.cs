using RPG_Game.Consumables;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class SwiftArmor : Item, IArmor
    {
        public SwiftArmor(int playerLevel) : base("Swift armor") {
            Agility = 10;


        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public override string ToString()
        {
            return $"Swift as the wind {Agility} agility, {Armor} armor";
        }
    }

    
    
}
