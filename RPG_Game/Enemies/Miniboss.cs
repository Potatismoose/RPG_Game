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
        }
        bool isBoss = true;

        public override string ToString()
        {
            return Type;
        }
    }
}
