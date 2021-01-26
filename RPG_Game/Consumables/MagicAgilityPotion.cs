using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Media;
using System.Text;

namespace RPG_Game.Consumables
{
    [Serializable]
    class MagicAgilityPotion: Potion
    {
        private int raiseAgility;
        public int RaiseAgility
        {
            get { return raiseAgility; }
            private set { raiseAgility = value; }
        }

        public override int TheChange { get; set; }




        public MagicAgilityPotion() : base("Magic agility potion")
        {
            RaiseAgility = 20;
            TheChange = RaiseAgility;
        }
        public override string Describe()
        {
            return $"Raise your agility during one fight. +{RaiseAgility}";
        }
        public override string ToString()
        {
            return $"COST: {Price}, GIVES: +{RaiseAgility} agility during one fight";
        }

        public override int Consume()
        {
            return RaiseAgility;
        }
    }
}
