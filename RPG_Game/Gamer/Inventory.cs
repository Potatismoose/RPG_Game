using RPG_Game.Consumables;
using RPG_Game.Interfaces;
using RPG_Game.Items;
using RPG_Game.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG_Game.Gamer
{
    [Serializable]
    class Inventory
    {
        private List<IInventoryable> equipment = new List<IInventoryable>();
        private List<IConsumable> consumable = new List<IConsumable>();
        private List<IAmulett> equippedAmuletts = new List<IAmulett>();
        private List<IArmor> equippedArmor = new List<IArmor>();
        private List<IShoes> equippedShoes = new List<IShoes>();
        private List<IWeapon> equippedWeapon = new List<IWeapon>();
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
        public string UnEquip(IEquippable thing, Player player)
        {
            
            equippedArmor.Add(new SwiftArmor(player.Level));
            if (thing is IWeapon tempWeapon)
            {
                for (int i = 0; i < equippedWeapon.Count; i++)
                {
                    if (tempWeapon.Name == equippedWeapon[i].Name)
                    {
                        equippedWeapon.RemoveAt(i);
                        thing.ActivateDeactivateEquipBool(false);
                    }
                };

            }
            else if (thing is IAmulett tempAmulett)
            {
                for (int i = 0; i < equippedAmuletts.Count; i++)
                {
                    if (tempAmulett.Name == equippedAmuletts[i].Name)
                    {
                        equippedAmuletts.RemoveAt(i);
                        thing.ActivateDeactivateEquipBool(false);
                    }
                };
            }

            else if (thing is IShoes tempShoe)
            {
                for (int i = 0; i < equippedShoes.Count; i++)
                {
                    if (tempShoe.Name == equippedShoes[i].Name)
                    {
                        equippedShoes.RemoveAt(i);
                        thing.ActivateDeactivateEquipBool(false);
                    }
                };
            }

            else if (thing is IArmor tempArmor)
            {

                for (int i = 0; i < equippedArmor.Count; i++)
                {
                    if (tempArmor.Name == equippedArmor[i].Name)
                    {
                        equippedArmor.RemoveAt(i);
                        thing.ActivateDeactivateEquipBool(false);
                    }
                };

            }
            return "";
        }
        public string Equip(IEquippable item, Player player)
        {


            if (item is IWeapon tempWeapon)
            {
                if (equippedWeapon.Count > 0 && equippedWeapon.Count < 2)
                {
                    for (int i = 0; i < equippedWeapon.Count; i++)
                    {
                        equippedWeapon[i].ActivateDeactivateEquipBool(false);
                        equippedWeapon.RemoveAt(i);
                        equippedWeapon.Add(tempWeapon);
                        if (equippedWeapon.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                            return $"{item.Name} has been equipped";
                        }

                    }
                }
                else if (equippedWeapon.Count == 0)
                {
                    equippedWeapon.Add(tempWeapon);
                    if (equippedWeapon.Contains(item))
                    {
                        item.ActivateDeactivateEquipBool(true);
                        return $"{item.Name} has been equipped";
                    }
                }
                else
                {
                    return "Error, something went wrong";

                }
                
            }
            else if (item is IAmulett tempAmulett)
            {
                if (equippedAmuletts.Count > 0 && equippedAmuletts.Count < 2)
                {
                    for (int i = 0; i < equippedAmuletts.Count; i++)
                    {
                        equippedAmuletts[i].ActivateDeactivateEquipBool(false);
                        equippedAmuletts.RemoveAt(i);
                        equippedAmuletts.Add(tempAmulett);
                        if (equippedAmuletts.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                            return $"{item.Name} has been equipped";
                        }

                    }
                }
                else if (equippedAmuletts.Count == 0)
                {
                    equippedAmuletts.Add(tempAmulett);
                    if (equippedAmuletts.Contains(item))
                    {
                        item.ActivateDeactivateEquipBool(true);
                        return $"{item.Name} has been equipped";
                    }
                }
                else
                {
                    return "Error, something went wrong";

                }
               
            }

            else if (item is IShoes tempShoes)
            {
                if (equippedShoes.Count > 0 && equippedShoes.Count < 2)
                {
                    for (int i = 0; i < equippedShoes.Count; i++)
                    {
                        equippedShoes[i].ActivateDeactivateEquipBool(false);
                        equippedShoes.RemoveAt(i);
                        equippedShoes.Add(tempShoes);
                        if (equippedShoes.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                            return $"{item.Name} has been equipped";
                        }
                        

                    }
                }
                else if (equippedShoes.Count == 0)
                {
                    equippedShoes.Add(tempShoes);
                    if (equippedShoes.Contains(item))
                    {
                        item.ActivateDeactivateEquipBool(true);
                        return $"{item.Name} has been equipped";
                    }
                }
                else
                {
                    return "Error, something went wrong";

                }
                
            }

            else if (item is IArmor tempArmor)
            {
                if (equippedArmor.Count > 0 && equippedArmor.Count < 2)
                {
                    
                    for (int i = 0; i < equippedArmor.Count; i++)
                    {
                        equippedArmor[i].ActivateDeactivateEquipBool(false);
                        equippedArmor.RemoveAt(i);
                        equippedArmor.Add(tempArmor);
                        if (equippedArmor.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                            return $"{item.Name} has been equipped";
                        }
                        

                    }
                }
                else if (equippedArmor.Count == 0)
                {
                    equippedArmor.Add(tempArmor);
                    if (equippedArmor.Contains(item))
                    {
                        item.ActivateDeactivateEquipBool(true);
                        return $"{item.Name} has been equipped";
                    }
                }
                else
                {
                    return "Error, something went wrong";

                }
               
            }

            return $"Error: {item.Name} has NOT been equipped";
        }


    }
}
