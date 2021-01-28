using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;

namespace RPG_Game.Items
{
    [Serializable]
    abstract class Item : IInventoryable, IEquippable, ISellable, IItem
    {

        private int price;
        public int Price
        {
            get { return price; }
            protected set { price = value; }
        }

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
            set { equipped = value; }
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
        protected Item(string name)
        {
            Name = name;
            Type = "Item";
        }

        public void ActivateDeactivateEquipBool(bool state)
        {

            Equipped = state;

        }


        public virtual string Describe()
        {
            return "It´s an Item";
        }

        public Dictionary<string, int> Sell(Player player, ISellable thing)
        {
            Dictionary<string, int> soldItem = new Dictionary<string, int>
            {
                { Name, Price }
            };
            player.TakeGold((int)Math.Round((double)Price * 0.8));
            return soldItem;
        }

        public string TheOriginalType()
        {
            return Type;
        }

        


    }
}
