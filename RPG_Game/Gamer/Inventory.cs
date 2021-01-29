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
        private List<IAmulett> equippedAmulett = new List<IAmulett>();
        private List<IArmor> equippedArmor = new List<IArmor>();
        private List<IShoes> equippedShoe = new List<IShoes>();
        private List<IWeapon> equippedWeapon = new List<IWeapon>();
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

        public string Equip(IEquippable item, Player player, bool remove)
        {

            /********************************************
                    CHANGE WEAPON
             ********************************************/
            if (item is IWeapon tempWeapon)
            {
                //If the player changes to another item of the same type, but do not unequip the first one before.
                if (!remove)
                {


                    if (equippedWeapon.Count == 0)
                    {
                        equippedWeapon.Add(tempWeapon);
                        player.StrengthWeapon += tempWeapon.Damage;
                        player.MakeVitalChangeAfterEquip("Weapon");
                        if (equippedWeapon.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                    }
                    else
                    {
                        equippedWeapon[0].ActivateDeactivateEquipBool(false);
                        player.StrengthWeapon -= player.StrengthWeapon * 2;
                        player.MakeVitalChangeAfterEquip("Weapon");
                        equippedWeapon.RemoveAt(0);
                        equippedWeapon.Add(tempWeapon);
                        if (equippedWeapon.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }

                        player.StrengthWeapon += tempWeapon.Damage;
                        player.MakeVitalChangeAfterEquip("Weapon");
                    }
                }
                //Else the Item is manually unequipped
                else
                {
                    for (int i = 0; i < equippedWeapon.Count; i++)
                    {
                        bool deleted = false;
                        if (!deleted)
                        {
                            if (equippedWeapon[i].Equals(item))
                            {
                                equippedWeapon[i].ActivateDeactivateEquipBool(false);


                                player.StrengthWeapon -= player.StrengthWeapon * 2;
                                equippedWeapon.RemoveAt(i);
                                player.MakeVitalChangeAfterEquip("Weapon");
                            }
                        }
                    }


                }
            }

            /********************************************
                    CHANGE SHOE
             ********************************************/
            if (item is IShoes tempShoe)
            {
                //If the player changes to another item of the same type, but do not unequip the first one before.
                if (!remove)
                {


                    if (equippedShoe.Count == 0)
                    {
                        equippedShoe.Add(tempShoe);
                        player.AgilityShoe += tempShoe.Agility;
                        player.MakeVitalChangeAfterEquip("Shoe");
                        if (equippedShoe.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                    }
                    
                    
                }
                //Else the Item is manually unequipped
                else
                {
                    for (int i = 0; i < equippedShoe.Count; i++)
                    {
                        bool deleted = false;
                        if (!deleted)
                        {
                            if (equippedShoe[i].Equals(item))
                            {
                                equippedShoe[i].ActivateDeactivateEquipBool(false);


                                player.AgilityShoe -= player.AgilityShoe * 2;
                                equippedShoe.RemoveAt(i);
                                player.MakeVitalChangeAfterEquip("Shoe");
                            }
                        }
                    }
                }
            }







            /********************************************
                    CHANGE AMULETT
             ********************************************/

            else if (item is IArmor tempArmor)
            {
                //If the player changes to another item of the same type, but do not unequip the first one before.
                if (!remove)
                {


                    if (equippedArmor.Count == 0)
                    {
                        equippedArmor.Add(tempArmor);

                        player.AgilityArmor += tempArmor.Agility;
                        player.ArmorArmor += tempArmor.Armor;

                        player.MakeVitalChangeAfterEquip("Armor");
                        if (equippedArmor.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                    }
                    else
                    {
                        equippedArmor[0].ActivateDeactivateEquipBool(false);

                        player.AgilityArmor -= player.AgilityArmor * 2;

                        player.ArmorArmor -= player.ArmorArmor * 2;
                        player.MakeVitalChangeAfterEquip("Armor");
                        equippedArmor.RemoveAt(0);
                        equippedArmor.Add(tempArmor);
                        if (equippedArmor.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                        player.AgilityArmor += tempArmor.Agility;

                        player.ArmorArmor += tempArmor.Armor;
                        player.MakeVitalChangeAfterEquip("Armor");
                    }
                }
                //Else the Item is manually unequipped
                else
                {
                    for (int i = 0; i < equippedArmor.Count; i++)
                    {
                        bool deleted = false;
                        if (!deleted)
                        {
                            if (equippedArmor[i].Equals(item))
                            {
                                equippedArmor[i].ActivateDeactivateEquipBool(false);
                                player.AgilityArmor -= player.AgilityArmor * 2;

                                player.ArmorArmor -= player.ArmorArmor * 2;
                                equippedArmor.RemoveAt(i);
                                player.MakeVitalChangeAfterEquip("Armor");
                            }
                        }
                    }


                }
            }
            /********************************************
                    CHANGE AMULETT
             ********************************************/

            else if (item is IAmulett tempAmulett)
            {
                //If the player changes to another item of the same type, but do not unequip the first one before.
                if (!remove)
                {


                    if (equippedAmulett.Count == 0)
                    {
                        equippedAmulett.Add(tempAmulett);

                        player.AgilityAmulett += tempAmulett.Agility;
                        player.HealthAmulett += tempAmulett.Hp;
                        player.StrengthAmulett += tempAmulett.Strength;
                        player.MakeVitalChangeAfterEquip("Amulett");
                        if (equippedAmulett.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                    }
                    else
                    {
                        equippedAmulett[0].ActivateDeactivateEquipBool(false);

                        player.AgilityAmulett -= player.AgilityAmulett * 2;
                        player.HealthAmulett -= player.HealthAmulett * 2;
                        player.StrengthAmulett -= player.StrengthAmulett * 2;
                        player.MakeVitalChangeAfterEquip("Amulett");
                        equippedAmulett.RemoveAt(0);
                        equippedAmulett.Add(tempAmulett);
                        if (equippedAmulett.Contains(item))
                        {
                            item.ActivateDeactivateEquipBool(true);
                        }
                        player.AgilityAmulett += tempAmulett.Agility;
                        player.HealthAmulett += tempAmulett.Hp;
                        player.StrengthAmulett += tempAmulett.Strength;
                        player.MakeVitalChangeAfterEquip("Amulett");
                    }
                }
                //Else the Item is manually unequipped
                else
                {
                    for (int i = 0; i < equippedAmulett.Count; i++)
                    {
                        bool deleted = false;
                        if (!deleted)
                        {
                            if (equippedAmulett[i].Equals(item))
                            {
                                equippedAmulett[i].ActivateDeactivateEquipBool(false);
                                player.AgilityAmulett -= player.AgilityAmulett * 2;
                                player.HealthAmulett -= player.HealthAmulett * 2;
                                player.StrengthAmulett -= player.StrengthAmulett * 2;
                                equippedAmulett.RemoveAt(i);
                                player.MakeVitalChangeAfterEquip("Amulett");
                            }
                        }
                    }


                }
            }
            return $"{item.Name} was equipped";

        }









    }
}
