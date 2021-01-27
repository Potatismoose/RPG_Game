using RPG_Game.Interfaces;

namespace RPG_Game.Items
{
    class StrengthAmulett : Item, IAmulett
    {
        public StrengthAmulett(string name) : base(name)
        {
            TheChange = 15;
            Strength = TheChange;
            Type = "Amulett";
        }

        public int Strength { get; private set; }
    }
}
