using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Consumables
{
    class MaxHealingPotion: Potion
    {
        private int restoreHp;
        public int RestoreHp
        {
            get { return restoreHp; }
            private set { restoreHp = value; }
        }
        private int playerMaxHp;

        public int PlayerMaxHp
        {
            get { return playerMaxHp; }
            private set { playerMaxHp = value; }
        }

        public MaxHealingPotion(int playerMaxHp, string name):base(name)
        {
            PlayerMaxHp = playerMaxHp;
            restoreHp = 10000;
        }
        public override string Describe()
        {
            return "Restore your Max HP (what was your max level at puchase).";
        }
        public override string ToString()
        {
            return $"COST: {Price}, RESTORES: +{PlayerMaxHp}";
        }

        public override int Consume()
        {
            throw new NotImplementedException();
        }
    }
}
