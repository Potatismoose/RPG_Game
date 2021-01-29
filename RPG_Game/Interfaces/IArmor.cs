namespace RPG_Game.Interfaces
{
    interface IArmor : IEquippable, IInventoryable
    {


        public int Agility { get; }
        public int Armor { get; }
        public int Price { get; }



    }
}
