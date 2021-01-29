using System;

namespace RPG_Game.Consumables
{
    [Serializable]
    class MaxHealingPotion : Potion
    {




        //set the values for the healing potion.
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
