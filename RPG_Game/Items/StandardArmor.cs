using RPG_Game.Interfaces;
using System;

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
            Price = 120;

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
