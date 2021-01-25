using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{   [Serializable]
    abstract class Item : IInventoryable, IEquipable, IShopable, IItem
    {
        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }
        private int theChange;
        public int TheChange
        {
            get { return theChange; }
            set { theChange = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            private set { type = value; }
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
