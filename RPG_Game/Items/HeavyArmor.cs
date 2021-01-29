using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class HeavyArmor : Item, IArmor
    {
        //Constructor for heavy armor
        public HeavyArmor(int playerLevel) : base("Heavy armor")
        {
            Agility = -5;
            Armor = 25;
            base.Type = "Item";
            Price = 480;
        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public override string Describe()
        {
            return $"It´s safe as a tank! Heavy as one too.";
        }
    }
}
