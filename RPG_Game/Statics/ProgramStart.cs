using RPG_Game.Soundhandling;
using System;
using System.Collections.Generic;
using System.Text;


namespace RPG_Game
{
    class ProgramStart
    {


        public void RunGame()
        {
            Menu menu = new Menu(new StartSound());
            menu.StartMenu();
        }
        
    }
}
