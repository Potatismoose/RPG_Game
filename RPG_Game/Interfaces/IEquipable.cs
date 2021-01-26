using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IEquipable
    {
        public void Equip();
        public string Describe();
        
        public bool Equipped { get; }
        
        public string Name { get; }
    }
}
