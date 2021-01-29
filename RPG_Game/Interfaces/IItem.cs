using RPG_Game.Gamer;
using RPG_Game.Items;

namespace RPG_Game.Interfaces
{
    interface IItem : IEquippable
    {

        public string Type { get; }
        public string Name { get; }
        public int TheChange { get; }
        public int Agility { get; }
        public int Price { get; }
        public bool Equipped { get; }
        



        
        public string Describe();



    }
}
