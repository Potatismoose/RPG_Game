using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Enemies
{
    class Dragon : Enemy
    {
        public Dragon(Player player) : base(player)
        {

        }
        bool isBoss = true;
    }
}
