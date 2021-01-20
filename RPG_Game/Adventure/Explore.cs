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
            if (adventureChoice <= 10)
            {
                Console.SetCursorPosition(45, 21);
                Print.Yellow("Seems to be a dead end. I´ll better go back");
                Console.SetCursorPosition(45, 22);
                Print.Yellow("Press enter to continue");
                
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.PlaySound(sounds[4]);
                Console.ReadKey();
                sound.Dispose();
            }
            else if (adventureChoice > 10 && adventureChoice < 13)
            {
                Console.SetCursorPosition(45, 21);
                Print.Red("You got bit by a snake and lost 5hp. At least you survived.");
                Console.SetCursorPosition(45, 22);
                Console.WriteLine("Press enter to continue");
                player.TakeDamage(5, false);
                Console.ReadKey();

            }
            else
            {

                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine fightMusic = new AudioPlaybackEngine();
                fightMusic.PlaySound(sounds[3]);
                Fight fight = new Fight(player);
                fight.PrintFight(new Dragon(player), player, fightMusic, _menuObject);
                fightMusic.Dispose();
                
                
            }
        }
    }
}
