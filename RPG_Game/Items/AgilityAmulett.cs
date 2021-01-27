using RPG_Game.Interfaces;

namespace RPG_Game.Items
{
    class AgilityAmulett : Item, IAmulett
    {
        public AgilityAmulett(string name) : base(name)
        {

            TheChange = 8;
            Agility = TheChange;
            Strength = 0;
            Type = "Amulett";
        }

        public int Strength { get; private set; }
    }
}
