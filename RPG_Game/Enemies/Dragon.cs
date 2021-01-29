using RPG_Game.Gamer;
using System;

namespace RPG_Game.Enemies
{
    class Dragon : Enemy
    {
        //Constructor for the dragon boss fight
        public Dragon(Player player) : base(player, "")
        {
            Random rand = new Random();

            Type = "Dragon";
            IsBoss = true;
            Agility = rand.Next(12, 16);
            Strength = rand.Next(90, 130);
            Shield = rand.Next(10, 15);
            Xp = 500;
            Gold = 2000;
            Health = rand.Next(400,651);


        }


        public override string Attack(Player player)
        {


            int damage = Strength;

            return player.TakeDamage(damage, true);
        }



        public override string ToString()
        {
            return Type;
        }
    }
}
