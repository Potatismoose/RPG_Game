using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class StrengthAmulett : Item, IAmulett
    {
        public StrengthAmulett(string name) : base(name)
        {
            TheChange = 15;
            Strength = TheChange;
            Type = "Item";
        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }
    }
}
