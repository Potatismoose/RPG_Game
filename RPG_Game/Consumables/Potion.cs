using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Consumables
{
    [Serializable]
    abstract class Potion : IConsumable, IInventoryable, IShopable
    {
        //Declaring some fields/properties
        private string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        private string type;
        public string Type
        {
            get { return type; }
            private set { type = value; }
        }

        private int price;
        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private int theChange;
        public virtual int TheChange
        {
            get { return theChange; }
            set { theChange = value; }
        }



        //Constructor
        public Potion(string name)
        {
            Price = 20;
            Name = name;
            Type = "Potion";
            

        }

        public virtual void BuyItem()
        {
            throw new NotImplementedException();
        }

        

        public virtual string Describe()
        {
            return "It´s a potion";
        }

        

        public abstract int Consume();

        public string theOriginalType()
        {
            return "Potion";
        }
        
    }

    
}
