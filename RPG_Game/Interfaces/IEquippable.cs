using RPG_Game.Gamer;

namespace RPG_Game.Interfaces
{
    interface IEquippable : IInventoryable
    {
        
        public string Describe();
        public bool Equipped { get; }
        public void ActivateDeactivateEquipBool(bool state);




    }
}
