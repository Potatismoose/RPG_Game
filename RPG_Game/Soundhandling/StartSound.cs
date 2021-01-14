using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Soundhandling
{
    class StartSound
    {
        
       
        public StartSound()
        {
            this.Click = new CachedSound(@$"click.mp3");
            this.Menu = new CachedSound(@$"menu.mp3");
            this.Fight = new CachedSound(@$"fight.mp3");
        }

        public CachedSound Click { get;}
        public CachedSound Menu { get; }
        public CachedSound Fight { get;}

        
    }
}
