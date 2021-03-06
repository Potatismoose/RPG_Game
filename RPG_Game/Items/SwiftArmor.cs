﻿using RPG_Game.Interfaces;
using System;

namespace RPG_Game.Items
{
    [Serializable]
    class SwiftArmor : Item, IArmor
    {
        //Constructor for swift armor
        public SwiftArmor(int playerLevel) : base("Swift armor")
        {
            Agility = 10;
            Armor = 8;
            base.Type = "Item";
            Price = 200;


        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public override string Describe()
        {
            return $"Swift and light. +{Armor}Armor +{Agility}Agility";
        }
        public override string ToString()
        {
            return $"Swift as the wind {Agility} Agility, {Armor} Armor";
        }
    }



}
