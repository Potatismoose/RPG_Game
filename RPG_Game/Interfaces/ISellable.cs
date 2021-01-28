using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface ISellable
    {
        
        public Dictionary<string,int> Sell(Player player, ISellable thing); 
        
    }
}
