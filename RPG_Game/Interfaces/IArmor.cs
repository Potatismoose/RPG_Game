namespace RPG_Game.Interfaces
{
    interface IArmor : IEquipable, IInventoryable
    {


        public int Agility { get; }
        public int Armor { get; }



    }
}
