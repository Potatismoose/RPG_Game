using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class FastShoes : Item
    {

        public FastShoes() : base("Fast shoes")
        {

            TheChange = 10;
            Agility = 10;
            base.Type = "Item";
        }

        public override string Describe()
        {
            return $"Light and durable shoes. You run so fast now.";
        }
    }
}
