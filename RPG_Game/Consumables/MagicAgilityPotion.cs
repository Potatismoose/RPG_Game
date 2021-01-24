using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace RPG_Game.Consumables
{
    class MagicAgilityPotion: Potion
    {
        private int raiseAgility;
        public int RaiseAgility
        {
            get { return raiseAgility; }
            private set { raiseAgility = value; }
        }

        bool used = false;
        public MagicAgilityPotion(string name) : base(name)
        {
            
        }
        public override string Describe()
        {
            return "You can raise your agility during one fight with this potion.";
        }
        public override string ToString()
        {
            return $"COST: {Price}, GIVES: +{RaiseAgility} agility during one fight";
        }
    }
}
