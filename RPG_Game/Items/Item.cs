using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace RPG_Game.Items
{   [Serializable]
    abstract class Item : IInventoryable, IEquipable, IShopable, IItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
        private bool equipped;
        public bool Equipped
        {
            get { return equipped; }
            protected set { equipped = value; }
        }

        private int agility;
        public int Agility
        {
            get { return agility; }
            protected set { agility = value; }
        }
        private int theChange;
        public int TheChange
        {
            get { return theChange; }
            protected set { theChange = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            protected set { type = value; }
        }

        //Constructor
        public Item(string name)
        {
            Name = name;
            Type = "Weapon";
        }

        public void Equip()
        {
           
        }

        public void BuyItem()
        {
            throw new NotImplementedException();
        }

        public string theOriginalType()
        {
            return "Item";
        }
    }
}
