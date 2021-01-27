using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class HeavyArmor : Item, IArmor
    {
        public HeavyArmor(int playerLevel) : base("Heavy armor")
        {
            Agility = -10;
            Armor = 15;
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
            return $"It´s safe as a tank! Heavy as one too. +{Armor} Armor, {Agility} Agility";
        }
    }
}
