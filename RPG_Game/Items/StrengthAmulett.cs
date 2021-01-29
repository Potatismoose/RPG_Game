using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class StrengthAmulett : Item, IAmulett
    {
        public StrengthAmulett() : base("Strength amulett")
        {
            TheChange = 15;
            Strength = TheChange;
            Type = "Item";
            Price = 150;
        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }
    }
}
