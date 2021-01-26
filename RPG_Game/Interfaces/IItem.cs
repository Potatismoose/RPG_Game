using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IItem
    {

        public string Type { get; }
        public string Name { get; }

        public int TheChange { get; }
        public int Agility { get; }
        public bool Equipped { get; }



        public virtual void Equip()
        { 
        }
    }
}
