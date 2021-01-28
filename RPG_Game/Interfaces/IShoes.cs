using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IShoes: IEquippable
    {
        public int Agility { get; }
        
    }
}
