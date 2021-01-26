using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Items
{
    [Serializable]
    class Backpack : IShopable
    {
        Inventory inventory;
        private string name;
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }

        

        private int theChange;
        public int TheChange
        {
            get { return theChange; }
            protected set { theChange = value; }
        }
        public string Type => throw new NotImplementedException();

        public Backpack(int space)
        {
            inventory = new Inventory(space);
            Name = "Backpack";
            TheChange = space;

        }
        public Backpack(int space, int healthPotion)
        {
            inventory = new Inventory(space, healthPotion);
            Name = "Backpack";
            TheChange = space;

        }

        public string AddToInventory(IInventoryable item)
        {
            return inventory.AddToInventory(item);
        }
        public string RemoveFromBackpack(IInventoryable item)
        {
            return inventory.RemoveFromInventory(item);
        }
        public List<IInventoryable> PrintAllItems()
        {
            return inventory.PrintAll();
        }
        public List<IConsumable> PrintAllItems(int noll)
        {
            return inventory.PrintAllItems(noll);
        }
        public List<IInventoryable> PrintAllItems(string item)
        {
            return inventory.PrintAllItems(item);
        }
        public void BuyItem()
        {
            throw new NotImplementedException();
        }
        public int ShowSpace()
        {
            return inventory.InventoryMaxLimit;
        }

        public void Equip()
        {
            throw new NotImplementedException();
        }
        public string InventoryStatus()
        {
            return inventory.ToString();
        }

        public string ShortInfoAboutInventoryStatus()
        {

            return inventory.ShortInfoAboutInventoryStatus();
        }
        public bool IsInventoryFull()
        {

            return inventory.IsInventoryFull();
        }
    }
}
