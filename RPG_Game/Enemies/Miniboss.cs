using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Enemies
{
    class Miniboss : Enemy
    {
        public Miniboss(Player player, string type) : base(player,"")
        {
            Type = type;
            Health += Health * 4;
            IsBoss = true;
            Strength *= 2;
            //Setting gold drop level for the miniboss
            if (player.Level == 3)
            {
                //If the player has under half HP of 
                //MAX when entering the fight, the gold drop will be bigger
                if (player.MaxHealth / 2 > player.Health)
                {
                    
                    Gold = player.Health * 5;
                }
                //Else gold will be double the health of the player.
                else
                {
                    Gold = player.Health * 2;
                }
            }
        }


        public override string ToString()
        {
            return Type;
        }
    }
}
