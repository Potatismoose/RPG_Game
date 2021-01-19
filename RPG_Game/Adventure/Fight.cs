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
        public Fight(Player player)
        {
            _player = player;
        }

        public Player Player { get; }

        public void PrintFight(Enemy enemy, Player player, AudioPlaybackEngine currentMusic)
        {
            do
            {
                if (player.Alive == true)
                {
                    string textToPrint = "";
                    Console.Clear();
                    int top = 11;
                    int left = 0;
                    Print.LogoPrint();
                    Print.DragonPrint();
                    player.PrintCurrentPlayerStatus();
                    Console.SetCursorPosition(left, top);

                    Console.ReadKey();
                    Print.WeaponAnimation(false);
                    textToPrint = player.Attack(enemy);
                    Console.WriteLine(textToPrint);
                    int deleteColumn = 105;
                    int deleteRow = 1;



                    Thread.Sleep(2000);
                    if (enemy.Alive)
                    {
                        Console.Clear();
                        Print.LogoPrint();
                        player.PrintCurrentPlayerStatus();
                        Print.DragonAnimation(player);
                        enemy.Attack(player);

                        if (player.Alive)
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                Console.SetCursorPosition(deleteColumn, deleteRow);
                                Console.Write(new string(' ', 30));
                                deleteRow++;
                            }

                            player.PrintCurrentPlayerStatus();
                            Print.Blue("Press enter to attack again");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Game Over");
                            Console.ReadKey();
                            currentMusic.PauseSound();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        currentMusic.PauseSound();
                        Console.WriteLine("VICTORY!!!");
                        player.TakeGold(enemy.DropGold());
                        player.TakeXp(enemy.GiveXp());
                        CachedSound win = new CachedSound(@$"fightwin.mp3");
                        AudioPlaybackEngine fightWin;
                        fightWin = new AudioPlaybackEngine();
                        fightWin.PlaySound(win);
                        Thread.Sleep(4000);
                        fightWin.Dispose();



                    }
                }
                else
                {
                    break;
                }
            } while (enemy.Alive);
            
            



        }


    }
}
