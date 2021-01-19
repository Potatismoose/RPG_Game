using System;
using System.Collections.Generic;
using System.Text;


namespace RPG_Game
{
    class ProgramStart
    {


        public void RunGame()
        {
            Console.SetWindowSize(140, 40);
            Console.Title = "Dragon Quest";
            
            Menu menu = new Menu();
            

            menu.StartMenu(menu);
        }
        
    }
}
