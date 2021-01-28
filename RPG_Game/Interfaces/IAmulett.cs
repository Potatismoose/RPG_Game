namespace RPG_Game.Interfaces
{
    interface IAmulett: IEquippable
    {
        public int Agility { get; }
        public int Strength { get; }
        public int Hp { get; }
    }
}
