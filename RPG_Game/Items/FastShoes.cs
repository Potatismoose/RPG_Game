using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class FastShoes : Item, IShoes
    {

        public FastShoes() : base("Fast shoes")
        {

            TheChange = 10;
            Agility = 10;
            Type = "Item";
            
        }

        public override string Describe()
        {
            return $"Light and durable shoes. You run so fast now.";
        }
    }
}
