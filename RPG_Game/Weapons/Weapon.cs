using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{
    abstract class Weapon : IInventoryable, IEquipable, IWeapon, IShopable
    {
        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            private set { type = value; }
        }

        private int damage;
        public int Damage
        {
            get { return damage; }
            private set { damage = value; }
        }

        private bool lootable;
        public bool Lootable
        {
            get { return lootable; }
            private set { lootable = value; }
        }

        private bool buyable;
        public bool Buyable
        {
            get { return buyable; }
            private set { buyable = value; }
        }

        //Constructor for weapons
        public Weapon(string name)
        {
            Name = name;
            Type = "Weapon";
        }


        //Methods below
        public void BuyItem()
        {
            throw new NotImplementedException();
        }
    }
}
