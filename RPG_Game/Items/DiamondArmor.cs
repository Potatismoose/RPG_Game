using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class DiamondArmor : Item, IArmor
    {
        //Constructor
        public DiamondArmor(int playerLevel) : base("Diamond armor")
        {
            Armor = 30;
            Agility = 5;
            Type = "Item";
        }


        private int armor;
        public int Armor
        {
            get { return armor; }
            private set { armor = value; }
        }





        public override string Describe()
        {
            return $"Withstands dragonflame!..it´s partly true.";
        }

        public override string ToString()
        {
            return $"The hardest of armors, +{Armor} armor";
        }
    }


}
