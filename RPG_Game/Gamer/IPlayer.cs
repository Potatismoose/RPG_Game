using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using RPG_Game.Enemies;

namespace RPG_Game.Gamer
{
     interface IPlayer
    {
        public string Name { get; }
        public int Health { get; }
        public int MaxHealth { get; }
        public int Strength { get; }
        public int Armor { get; }
        public int Gold { get; }
        public int Level { get;  }
        public int Agility { get; }
        public int StrengthAmulett { get; }
        public int LuckyDamage { get; }
        public bool Alive { get; }
        public int Xp { get; }
        public int Threshold { get; }

        public string Attack(Enemy enemy);
        public string TakeDamage(int damage, bool enemyAttack);
        public int TakeGold(int gold);
        int TakeXp(int xp, Menu _menuObject);

    }
}
