using RPG_Game.Gamer;
using RPG_Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Consumables
{   [Serializable]
    class HealingPotion: Potion
    {
        private int restoreHp;
        public int RestoreHp
        {
            get { return restoreHp; }
            private set { restoreHp = value; }
        }

        public HealingPotion(int playerMaxHp,string name) : base(name)
        {
            restoreHp = (int)Math.Round((double)playerMaxHp/6);
            TheChange = RestoreHp;
        }

        public override string Describe()
        {
            return "You can heal yourself after or during battles with this potion";
        }
        public override string ToString()
        {
            return $"COST: {Price}, RESTORES: +{RestoreHp}Hp";
        }

        public override void Consume(Player player)
        {
            player.RestoreHp(RestoreHp);
        }
    }
}
