using System;

namespace RPG_Game.Weapons
{
    [Serializable]
    class DevilsBlade : Weapon
    {
        public DevilsBlade() : base("Devils blade", 49)
        {
            Price = 490;
        }
        public override string ToString()
        {
            return $"From hells forges, {Damage} damage";
        }

        public override string Describe()
        {
            return $"From hells forges, {Damage} damage";
        }
    }
}
