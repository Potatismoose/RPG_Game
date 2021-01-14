using System;
using System.Collections.Generic;
using System.Text;
using System.Media;
using NAudio.Wave;
using System.Threading;
using RPG_Game.Gamer;
using RPG_Game.Soundhandling;

namespace RPG_Game
{
     class Menu
    {
        private List<Player> ListPlayer = new List<Player>();
        private int top = 13;
        private int left = 45;
        string[] startMenuOptions = new string[2] { "New game", "Continue your adventure" };

        public StartSound sounds { get; }
        StartSound soundInitilizer = new StartSound();
        public Menu(StartSound sounds)
        {
            this.sounds = sounds;
        }

        public void StartMenu()
        {
            
            Console.Clear();
            
            AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Menu);

            Print.LogoPrint();
            Print.DragonPrint();
            

            for (int i = 0; i < startMenuOptions.Length; i++)
            {
                Console.SetCursorPosition(left, top);
                Console.WriteLine($"{i + 1}. {startMenuOptions[i]}");
                top++;
            }
            
            Console.SetCursorPosition(left, top + 1);
            Console.Write("Choose your option: ");
            string option = Console.ReadLine();
            AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Click);
            NewGame();

        }

        private void NewGame()
        {
            Thread.Sleep(500);
  
            top = 13;
            Console.Clear();
            Print.LogoPrint();
            Print.DragonPrint();
            Console.SetCursorPosition(left, top);
            Console.Write("What is our heros name?> ");
            string name = Console.ReadLine();
            AudioPlaybackEngine.Instance.PlaySound(soundInitilizer.Click);
            ListPlayer.Add(new Player(name));
            
            


        }

        private void InGameMenu()
        {

        }

        
    }

}
