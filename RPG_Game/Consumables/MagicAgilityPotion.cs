using System;

namespace RPG_Game.Consumables
{
    [Serializable]
    class MagicAgilityPotion : Potion
    {
        private int raiseAgility;
        public int RaiseAgility
        {
            get { return raiseAgility; }
            private set { raiseAgility = value; }
        }

        public override int TheChange { get; set; }


        //set the values for the magic agility potion.

        public MagicAgilityPotion() : base("Magic agility potion", 120)
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
