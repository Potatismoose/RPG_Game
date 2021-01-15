using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Enemies;

namespace RPG_Game.Adventure
{
    class Explore
    {

        public void GoAdventure(Player player)
        {
            Random rand = new Random();
            int adventureChoice = rand.Next(1, 101);
            if (adventureChoice <= 10)
            {
                Console.WriteLine("Nothing out there, seems to be a dead end. I´ll better go back");
            }
            else if (adventureChoice > 10 && adventureChoice < 13)
            {
                Console.WriteLine("You got bit by a snake and lost 5hp. At least you survived.");
                player.TakeDamage(5, false);
                Console.WriteLine("Your current status is now:");

            }
            else
            {
                Console.WriteLine("FIGHT!!");
                Fight fight = new Fight();
                Console.ReadKey();
            }
        }
    }
}
