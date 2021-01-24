using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Enemies
{
    class Miniboss : Enemy
    {
        private int shield;

        public int Shield
        {
            get { return shield; }
            private set { shield = value; }
        }

        public Miniboss(Player player, string type) : base(player,"")
        {
            Type = type;
            Health += Health * 4;
            IsBoss = true;
            Strength *= 2;
            if (type == "Evil Minotaur")
            {
                Shield = 20;
            }
            else 
            {
                Shield = 0;
            }
            //Setting gold drop level for the miniboss
            switch (player.Level) 
            {
                case 3:
                case 6:
                //If the player has under half HP of 
                //MAX when entering the fight, the gold drop will be bigger
                if (player.Name.ToLower() == "robin")
                {
                    Gold = Health;
                }
                else if (player.MaxHealth / 2 > player.Health)
                {
                    
                    Gold = player.Health * 5;
                }
                //Else gold will be double the health of the player.
                else
                {
                    Gold = player.Health * 2;
                }  
                    break;

            }
            
        }

        public override void TakeDamage(StringBuilder textToReturn, int damage, bool lucky, int luckyDamage)
        {
            bool evaded = false;
            Random rand = new Random();
            if (rand.Next(1, 101) <= Agility)
            {
                textToReturn.AppendLine(@$"{ToString()} evaded your attack.");
                evaded = true;
            }
            if (rand.Next(1, 101) <= Shield)
            {
                textToReturn.AppendLine(@$"{ToString()} used protective barrier");
                evaded = true;
            }
            else
            {
                if (!lucky)
                {
                    Health -= (damage);
                    if (Health <= 0)
                    {
                        Alive = false;
                    }
                }
            }
            if (lucky && !evaded)
            {
                textToReturn.AppendLine($"CRITICAL HIT! You dealt {damage + luckyDamage} damage");
                Health -= (damage + luckyDamage);
                if (Health <= 0)
                {
                    Alive = false;
                }
            }
            else if (!evaded)
            {
                textToReturn.AppendLine($"You dealt {damage} damage to the {ToString()}.");
            }
        }

        public override string ToString()
        {
            return Type;
        }
    }
}
