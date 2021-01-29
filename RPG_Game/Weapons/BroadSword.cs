using System;

namespace RPG_Game.Weapons
{
    [Serializable]
    class BroadSword : Weapon
    {
        public BroadSword() : base("Broad sword", 15)
        {
            Price = 190;
        }

        public override string ToString()
        {
            return $"Standard sword with {Damage} damage";
        }

        public override string Describe()
        {
            return $"Standard sword with {Damage} damage";
        }
    }
}
