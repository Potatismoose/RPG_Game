using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class FastShoes : Item
    {
        
        public FastShoes() : base("Fast shoes") {

            TheChange = 20;
        }
    }
}
