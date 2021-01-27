using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class SwiftArmor : Item, IArmor
    {
        public SwiftArmor(int playerLevel) : base("Swift armor")
        {
            Agility = 10;
            Armor = 8;
            base.Type = "Armor";

        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public override string Describe()
        {
            return $"Swift and light. +{Armor}Armor +{Agility}Agility";
        }
        public override string ToString()
        {
            return $"Swift as the wind {Agility} Agility, {Armor} Armor";
        }
    }



}
