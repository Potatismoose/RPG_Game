using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class AgilityAmulett : Item, IAmulett
    {
        public AgilityAmulett(string name) : base(name)
        {

            Agility = 8;
            Strength = 0;
            Type = "Item";
            Price = 80;
            
        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }

        public virtual string Describe()
        {
            return "The agility amulett makes you swift and fast";
        }
    }
}
