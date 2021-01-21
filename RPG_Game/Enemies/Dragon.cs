using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Enemies
{
    class Dragon : Enemy
    {
        public Dragon(Player player) : base(player, "")
        {
            Random rand = new Random();
            Type = "Dragon";
            
        }
        
        bool isBoss = true;

        
        public override string ToString()
        {
            return Type;
        }

        public override string Attack(Player player)
        {
            Random rand = new Random();

            int damage = Strength;

            return player.TakeDamage(damage, true);
        }

        public override void TakeDamage(StringBuilder textToReturn, int damage, bool lucky, int luckyDamage)
        {
            base.TakeDamage(textToReturn, damage, lucky, luckyDamage);
        }
    }
}
