using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class AgilityAmulett : Item, IAmulett
    {
        public AgilityAmulett(string name) : base(name)
        {

            TheChange = 8;
            Agility = TheChange;
            Strength = 0;
            Type = "Item";
        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }
    }
}
