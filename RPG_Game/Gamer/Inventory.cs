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
        public string RemoveFromInventory(string name)
        {
            string returnvalue = default(string);
            for (int i = 0; i < equipment.Count; i++)
            {
                bool removed = false;
                if (equipment[i].Name == name && !removed)
                {
                    returnvalue = equipment[i].Name;
                    equipment.Remove(equipment[i]);
                    removed = true;
                }
            }
            return returnvalue;
        }
        public void PrintAll()
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            int counter = 1;
            foreach (var item in equipment)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.WriteLine($"[{counter}] {item.Name}");
                cursorTop++;
                counter++;
            }
        }

        public List<IConsumable> PrintAll(int noll)
        {
            consumable.Clear();
            foreach (var item in equipment.Where(x => x.Type.Equals("Potion")))
            {
                consumable.Add((IConsumable)item);
            }
            return consumable;
        }
        public void PrintAll(string itemType)
        {
            int cursorTop = Console.CursorTop;
            int cursorLeft = Console.CursorLeft;
            int counter = 1;
            foreach (var item in equipment.Where(x => x.Type.Equals(itemType)))
            {
                Console.SetCursorPosition(cursorLeft, cursorTop);
                Console.WriteLine($"[{counter}] {item.Name}");
                cursorTop++;
                counter++;
            }
        }

    }
}
