namespace RPG_Game.Interfaces
{
    interface IWeapon : IEquippable
    {
        public new string Name { get; }
        public string Type { get; }
        public int Damage { get; }
        public bool Lootable { get; }
        public bool Buyable { get; }
        public int TheChange { get; }
        public int Price { get; }
    }
}
