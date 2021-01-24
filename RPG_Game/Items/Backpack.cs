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
            set { name = value; }
        }

        public Backpack(int space)
        {
            inventory = new Inventory(space);
            Name = "Backpack";

        }

        public string AddToInventory(IInventoryable item)
        {
            return inventory.AddToInventory(item);
        }
        public string RemoveFromBackpack(string name)
        {
            return inventory.RemoveFromInventory(name);
        }
        public void PrintAllItems()
        {
            inventory.PrintAll();
        }
        public List<IConsumable> PrintAllItems(int noll)
        {
            return inventory.PrintAll(noll);
        }
        public void PrintAllItems(string item)
        {
            inventory.PrintAll(item);
        }
        public void BuyItem()
        {
            throw new NotImplementedException();
        }
        public int ShowSpace()
        {
            return inventory.InventoryMaxLimit;
        }
    }
}
