using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class StrengthAmulett : Item, IAmulett
    {
        //Constructor for strength amulett
        public StrengthAmulett() : base("Strength amulett")
        {

            Strength = 15;
            Type = "Item";
            Price = 180;
        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }
    }
}
