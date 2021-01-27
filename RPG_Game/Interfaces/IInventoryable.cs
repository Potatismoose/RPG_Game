namespace RPG_Game.Interfaces
{
    interface IInventoryable
    {
        public string Name { get; }

        public string Type { get; }
        public int TheChange { get; }
        public string TheOriginalType();

    }
}
