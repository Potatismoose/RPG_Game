using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    public interface IConsumable
    {

        public string Name { get; }
        public int Price { get; }
        public int TheChange { get; }

        public abstract int Consume();
        public void BuyItem();
        public string Describe();
        
        //public void SellItem();
    }
}
