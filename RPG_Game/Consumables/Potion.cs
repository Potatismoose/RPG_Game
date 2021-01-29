using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Consumables
{
    [Serializable]
    abstract class Potion : IConsumable, IInventoryable//, ISellable
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
            protected set { price = value; }
        }

        private int theChange;
        public virtual int TheChange
        {
            get { return theChange; }
            set { theChange = value; }
        }



        //Constructor
        public Potion(string name, int price)
        {
            Price = price;
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

        //Not used at the moment, but will be implemented soon.
        //public Dictionary<string,int> Sell(Player player, ISellable thing)
        //{
        //    Dictionary<string, int> soldItem = new Dictionary<string, int>
        //    {
        //        { Name, Price }
        //    };
        //    player.TakeGold((int)Math.Round((double)Price * 0.8));
        //    return soldItem;
        //}

        public string TheOriginalType()
        {
            return Type;
        }
    }


}
