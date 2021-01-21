using RPG_Game.Enemies;
using RPG_Game.Gamer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPG_Game.Adventure
{
    class Fight
    {
        
        Player _player;
        List<string> _fightText;
        public Fight(Player player)
        {
            _player = player;
        }

        public Player Player { get; }
        
        public void PrintFight(Enemy enemy, Player player, AudioPlaybackEngine currentMusic, Menu _menuObject)
        {
            
            
            int top = 11;
            int left = 0;
            int leftMoveHere = default(int);
            int topMoveHere = default(int);
            List<string> fightText = new List<string>();
            _fightText = fightText;
            do
            {
                fightText.Clear();
                if (player.Alive == true)
                {
                                        
                    if (leftMoveHere != 0)
                    {
                        Print.ClearAllScreen(leftMoveHere,topMoveHere);
                    }
                    else { Print.ClearAllScreen(); }

                    if (enemy.ToString() == "Dragon")
                    {
                        Print.DragonPrint();
                    }
                    else 
                    {
                        Print.EnemyPrint(enemy.Type);
                    }
                    player.PrintCurrentPlayerStatus();
                    Print.FightConsole();
                    Print.FightConsolePrintText(fightText, player, enemy);
                    Console.SetCursorPosition(left, top);

                    if (enemy.ToString() == "Dragon")
                    {
                        Print.WeaponAnimation(false, _menuObject);
                    }
                    
                    fightText.Add(player.Attack(enemy));
                    Print.FightConsolePrintText(fightText, player, enemy);
                    leftMoveHere = Console.CursorLeft;
                    topMoveHere = Console.CursorTop;



                    Thread.Sleep(1000);
                    if (enemy.Alive)
                    {
                        if (enemy.ToString() == "Dragon")
                        {
                            Print.ClearAllScreen();
                            player.PrintCurrentPlayerStatus();
                            Print.FightConsolePrintText(fightText, player, enemy);
                            Print.DragonAnimation(player, enemy, fightText, _menuObject);
                        }
                        
                        fightText.Add(enemy.Attack(player));
                        Print.FightConsolePrintText(fightText, player, enemy);
                        leftMoveHere = Console.CursorLeft;
                        topMoveHere = Console.CursorTop;
                        if (player.Alive)
                        {


                            Print.PlayerStatsPrint(player);

                        }
                        else
                        {
                            
                            currentMusic.PauseSound();
                        }
                    }
                    else
                    {
                        
                        currentMusic.PauseSound();
                        currentMusic.Dispose();
                        Console.SetCursorPosition(0, 35);
                        Print.Green("VICTORY!!!");
                        Print.Green($"You looted the enemy and got {player.TakeGold(enemy.DropGold())} gold");
                        int currentLevel = player.Level;
                        Print.Green($"You also got {player.TakeXp(enemy.GiveXp(), _menuObject)} XP");
                        
                        CachedSound win = new CachedSound(@$"fightwin.mp3");
                        AudioPlaybackEngine fightWin;
                        fightWin = new AudioPlaybackEngine();
                        fightWin.PlaySound(win);
                        Thread.Sleep(4000);
                        fightWin.Dispose();


                        if (currentLevel < player.Level)
                        {
                            Console.Write("LEVEL UP! Press enter to continue.");
                            leftMoveHere = Console.CursorLeft;
                            topMoveHere = Console.CursorTop;
                            var sounds = _menuObject.SoundList();
                            AudioPlaybackEngine sound = new AudioPlaybackEngine();
                            sound.PlaySound(sounds[5]);
                            Print.PlayerStatsPrint(player);
                            Thread.Sleep(500);
                            sound.Dispose();
                            
                        }
                        else {
                            Console.Write("Press enter to continue.");
                            leftMoveHere = Console.CursorLeft;
                            topMoveHere = Console.CursorTop;
                            Print.PlayerStatsPrint(player);
                            
                            
                        }
                        
                        



                    }
                    Console.SetCursorPosition(leftMoveHere, topMoveHere);

                    leftMoveHere = default(int);
                    topMoveHere = default(int);
                    Console.ReadKey();
                }
                else
                {
                    break;
                }
            } while (enemy.Alive);
            
            



        }


    }
}
