using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IWeapon
    {
        public string Name { get; }
        public string Type { get; }
        public int Damage { get; }
        public bool Lootable { get; }
        public bool Buyable { get; }
    }
}
