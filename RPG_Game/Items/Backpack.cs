using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;

namespace RPG_Game.Items
{

    //This class is essentially useless. I could have skipped this and redirected everything to inventory class.
    //A work for refactoring. This class just works as a middle hand between player and inventory.
    [Serializable]
    class Backpack
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
        //Constructor for Backpack
        public Backpack(int space)
        {
            inventory = new Inventory(space);
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

        public void BuyItem()
        {
            throw new NotImplementedException();
        }
        public int ShowSpace()
        {
            return inventory.InventoryMaxLimit;
        }

        public string Equip(IEquippable thing, Player player, bool remove)
        {
            return inventory.Equip(thing, player, remove);
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
