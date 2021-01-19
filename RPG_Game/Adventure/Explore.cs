using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Enemies;


namespace RPG_Game.Adventure
{
    class Explore
    {

        public void GoAdventure(Player player, Menu _menuObject)
        {
            Random rand = new Random();
            int adventureChoice = rand.Next(1, 101);
            if (adventureChoice <= 50)
            {
                
                Console.WriteLine("Seems to be a dead end. I´ll better go back");
                Console.WriteLine("Press enter to continue");
                Console.ReadKey();
                
            }
            else if (adventureChoice > 50 && adventureChoice < 60)
            {
                Console.WriteLine("You got bit by a snake and lost 5hp. At least you survived.");
                player.TakeDamage(5, false);
                Console.ReadKey();

            }
            else
            {

                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine fightMusic = new AudioPlaybackEngine();
                fightMusic.PlaySound(sounds[3]);
                Fight fight = new Fight(player);
                fight.PrintFight(new Dragon(player), player, fightMusic);
                fightMusic.Dispose();
                
            }
        }
    }
}
