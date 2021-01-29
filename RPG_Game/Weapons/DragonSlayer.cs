using System;

namespace RPG_Game.Weapons
{
    [Serializable]
    class DragonSlayer : Weapon
    {
        public DragonSlayer() : base("Dragon slayer", 85)
        {
            Price = 750;
        }

        public override string ToString()
        {
            return $"Cuts thru dragon skin, {Damage} damage";
        }

        public override string Describe()
        {
            return $"Cuts thru dragon skin, {Damage} damage";
        }
    }
}
