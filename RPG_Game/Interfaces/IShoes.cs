using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IShoes: IEquippable, IItem
    {
        public int Agility { get; }
        
        
    }
}
