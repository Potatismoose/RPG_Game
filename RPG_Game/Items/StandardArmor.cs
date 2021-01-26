using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class StandardArmor : Item, IArmor
    {
        public StandardArmor(int playerLevel) : base("Standard armor") 
        {
            Armor = 5;
            Agility = 0;
            base.Type = "Item";

        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public override string Describe()
        {
            return $"Protects well against herpes +{Armor} Armor";
        }
    }
}
