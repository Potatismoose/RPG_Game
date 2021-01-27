using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Weapons;
using RPG_Game.Items;
using RPG_Game.Consumables;
using System.Linq;

namespace RPG_Game.Gamer
{[Serializable]
    class Inventory
    {
        private List<IInventoryable> equipment = new List<IInventoryable>();
        private List<IConsumable> consumable = new List<IConsumable>();
        private List<IEquipable> equipable = new List<IEquipable>();
        private int inventoryMaxLimit;
        public int InventoryMaxLimit
        {
            get { return inventoryMaxLimit; }
            private set { inventoryMaxLimit = value; }
        }
        
        //Constructor
        public Inventory(int space, int healthPotion)
        {
            InventoryMaxLimit = space;
            equipment.Add(new HealingPotion(healthPotion));
            
        }
        public Inventory(int space)
        {
            InventoryMaxLimit = space;
            
        }

        public string PrintCurrentStatusOfInventory()
        {
            return $"Backpack status: {equipment.Count}/{inventoryMaxLimit}";
        }
        public string AddToInventory(IInventoryable item)
        {
            if (equipment.Count < inventoryMaxLimit)
            {
                equipment.Add(item);
                return $"{item.Name} has been added to your backpack.";
            }
            else
            {
                return $"Your backpack is full. Limit: {inventoryMaxLimit}/{inventoryMaxLimit}";
            }
        }
        public string RemoveFromInventory(IInventoryable item)
        {
            Item it;
            Weapon we;
            Potion po;
           

            switch (item.TheOriginalType())
            {
                case "Weapon":
                    we = (Weapon)item;
                    RemoveThisItem(item);
                    break;
                case "Item":
                    it = (Item)item;
                    RemoveThisItem(item);
                    break;
                case "Potion":
                    po = (Potion)item;
                    RemoveThisItem(item);
                    break;
                default:
                    break;
            }
            return "";
            
        }

        private void RemoveThisItem(IInventoryable item)
        {
            bool removed = false;
            foreach (var inventoryItem in equipment.Where(x => x.Name == item.Name && x.TheChange == item.TheChange))
            {
                if (!removed)
                {
                    equipment.Remove(item);
                    break;
                }
                
            }
        }
        public List<IInventoryable> PrintAll()
        {
            return equipment;
        }

        public List<IConsumable> PrintAllItems(int noll)
        {
            consumable.Clear();
            foreach (var item in equipment.Where(x => x.Type.Equals("Potion")))
            {
                consumable.Add((IConsumable)item);
            }
            return consumable;
        }
        public List<IInventoryable> PrintAllItems(string itemType)
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            int counter = 1;
            List<IInventoryable> listOfInventory = new List<IInventoryable>();
            var test = equipment.Where(x => x.Type.Equals(itemType)).ToList();
            foreach (var item in equipment.Where(x => x.Type.Equals(itemType)))
            {
                listOfInventory.Add(item);
            }
            

            return listOfInventory;
        }

        public string ShortInfoAboutInventoryStatus()
        {

            return $"{equipment.Count}/{InventoryMaxLimit} items.";
        }
        public bool IsInventoryFull()
        {
            if (inventoryMaxLimit - equipment.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override string ToString()
        {
            
            return $"The inventory contains {equipment.Count}/{InventoryMaxLimit} items.";
        }
    }
}
