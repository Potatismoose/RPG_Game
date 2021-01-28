using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;

namespace RPG_Game.Weapons
{
    [Serializable]
    abstract class Weapon : IInventoryable, IWeapon, ISellable
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

        private bool equipped;

        public bool Equipped
        {
            get { return equipped; }
            protected set { equipped = value; }
        }


        //Constructor for weapons
        protected Weapon(string name, int damage)
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


        public string Equip(Player player, IEquippable item)
        {
            throw new NotImplementedException();
        }

        public string Describe()
        {
            return "Text om item här";
        }

        public Dictionary<string, int> Sell(Player player, ISellable thing)
        {
            Dictionary<string, int> soldItem = new Dictionary<string, int>
            {
                { Name,Price}
            };
            player.TakeGold((int)Math.Round((double)Price * 0.8));
            return soldItem;
        }

        public string TheOriginalType()
        {
            return Type;
        }

        public void ActivateDeactivateEquipBool(bool state)
        {
            Equipped = state;
        }
    }
}
