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
                Console.Write("Press enter to continue");
                
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.PlaySound(sounds[4]);
                Console.ReadKey();
                sound.Dispose();
            }
            else if (adventureChoice > 10 && adventureChoice <= 15)
            {
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine sound = new AudioPlaybackEngine();
                sound.RaiseVol();
                sound.PlaySound(sounds[11]);

                player.TakeDamage(5, false);
                Console.SetCursorPosition(45, 21);
                if (!player.Alive)
                {
                    Print.Red("You got bit by a snake and died.");
                }
                else {
                    Print.Red("You got bit by a snake and lost 5hp. At least you survived.");
                }
                
                Console.SetCursorPosition(45, 22);
                
                
                Console.Write("Press enter to continue");
                

                
                
                Console.ReadKey();
                sound.LowerVol();
                sound.Dispose();
            }
            else
            {
                var sounds = _menuObject.SoundList();
                AudioPlaybackEngine fightMusic = new AudioPlaybackEngine();
                fightMusic.PlaySound(sounds[3]);
                Fight fight = new Fight(player);
                
                List<string> enemyList = new List<string>() { "Devil", "Dragonpig", "Skeleton", "Axed goblin", "Tina the cat devil", "Bat", "Gummy bear"};
                List<string> bossList = new List<string>() { "Hell raised dragon", "Evil minotaur", "Long footed muppet" };
                //Calculates the level of XP that next enemy can drop, 
                //and if remaining XP for player to next level is below maximum XP drop, 
                //the boss is created
                if (player.Level == 3 && player.NextLevel - player.Xp <= 40 || player.Level == 6 && player.NextLevel - player.Xp <= 147)
                {

                    fight.PrintFight(new Miniboss(player, bossList[rand.Next(0, bossList.Count)]), player, fightMusic, _menuObject);
                }
                else if (player.Level == 9 && player.NextLevel - player.Xp <= 238)
                {
                    fight.PrintFight(new Dragon(player), player, fightMusic, _menuObject);
                }
                else
                {
                    fight.PrintFight(new Enemy(player, enemyList[rand.Next(0, enemyList.Count)]), player, fightMusic, _menuObject);
                }

                
                
                fightMusic.Dispose();
                
                
            }
        }
    }
}
