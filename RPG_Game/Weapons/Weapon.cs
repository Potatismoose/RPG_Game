using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Weapons
{   [Serializable]
    abstract class Weapon : IInventoryable, IEquipable, IWeapon, IShopable
    {
        private string name;
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            protected set { type = value; }
        }

        private int damage;
        public int Damage
        {
            get { return damage; }
            protected set { damage = value; }
        }

        private int theChange;
        public int TheChange
        {
            get { return theChange; }
            protected set { theChange = value; }
        }

        private bool lootable;
        public bool Lootable
        {
            get { return lootable; }
            protected set { lootable = value; }
        }

        private int price;
        public int Price
        {
            get { return price; }
            protected set { price = value; }
        }

        private bool buyable;
        public bool Buyable
        {
            get { return buyable; }
            protected set { buyable = value; }
        }

        //Constructor for weapons
        public Weapon(string name, int damage)
        {
            Name = name;
            Type = "Weapon";
            Damage = damage;
            TheChange = damage;
        }


        //Methods below
        public void BuyItem()
        {
            throw new NotImplementedException();
        }
        public string theOriginalType()
        {
            return "Weapon";
        }

    }
}
