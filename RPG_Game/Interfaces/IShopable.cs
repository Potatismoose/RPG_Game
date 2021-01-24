using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IShopable
    {
        public string Name { get; }
        public void BuyItem();
    }
}
