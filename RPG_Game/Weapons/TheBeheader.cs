using System;

namespace RPG_Game.Weapons
{
    [Serializable]
    class TheBeheader : Weapon
    {
        public TheBeheader() : base("The beheader", 35)
        {
            Price = 395;

        }
        public override string ToString()
        {
            return $"Sharp as a knife with orc steel, {Damage} damage";
        }

        public override string Describe()
        {
            return $"Sharp as a knife with orc steel, {Damage} damage";
        }
    }
}
