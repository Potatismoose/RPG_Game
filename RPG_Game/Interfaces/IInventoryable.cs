using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Game.Interfaces
{
    interface IInventoryable
    {
        public string Name{ get; }

        public string Type { get; }
    }
}
