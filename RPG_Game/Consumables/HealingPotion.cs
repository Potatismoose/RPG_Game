using System;

namespace RPG_Game.Consumables
{
    [Serializable]
    class HealingPotion : Potion
    {
        private int restoreHp;
        public int RestoreHp
        {
            get { return restoreHp; }
            private set { restoreHp = value; }
        }
        //set the values for the healing potion.
        public HealingPotion(int playerMaxHp, string name) : base(name, 20)
        {
            restoreHp = (int)Math.Round((double)playerMaxHp / 6);
            TheChange = RestoreHp;
        }
        public HealingPotion(int restore) : base("Healing potion", 20)
        {
            RestoreHp = restore;
            TheChange = RestoreHp;
        }

        public override string Describe()
        {
            return $"Restore HP. It heals up to {RestoreHp}Hp";
        }
        public override string ToString()
        {
            return $"COST: {Price}, RESTORES: +{RestoreHp}Hp";
        }



        public override int Consume()
        {
            return restoreHp;
        }
    }
}
