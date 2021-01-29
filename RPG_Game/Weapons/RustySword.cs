using System;

namespace RPG_Game.Weapons
{
    [Serializable]
    class RustySword : Weapon
    {
        public RustySword() : base("Rusty sword", 5)
        {
            Price = 90;
        }

        public override string ToString()
        {
            return $"Shitty sword that breaks if you look at it, {Damage} damage";
        }

        public override string Describe()
        {
            return $"Shitty sword that breaks if you look at it, {Damage} damage";
        }
    }
}
