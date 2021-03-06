﻿using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class AgilityAmulett : Item, IAmulett
    {
        //Constructor for amulett
        public AgilityAmulett() : base("Agility amulett")
        {

            Agility = 8;

            Type = "Item";
            Price = 110;

        }

        public int Strength { get; private set; }
        public int Hp { get; private set; }

        public virtual string Describe()
        {
            return "The agility amulett makes you swift and fast";
        }
    }
}
