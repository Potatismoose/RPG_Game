using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Consumables
{   [Serializable]
    class MaxHealingPotion : Potion
    {



        

        public MaxHealingPotion(int playerMaxHp) : base("Max healing potion", 120)
        {
            
            TheChange = playerMaxHp;
            

        }
        public override string Describe()
        {
            return "Restore your Max HP";
        }
        public override string ToString()
        {
            return $"COST: {Price}, RESTORES: +{TheChange}";
        }

        public override int Consume()
        {
            
            return TheChange;
        }
    }
}
