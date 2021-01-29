using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;
using RPG_Game.Interfaces;
namespace RPG_Game.Enemies
{
    class Enemy : IEnemy
    {
        private string type;
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private int health;
        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        private bool isBoss;
        public bool IsBoss
        {
            get { return isBoss; }
            set { isBoss = value; }
        }

        private int strength;
        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        private int gold;
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        private int shield;
        public int Shield
        {
            get { return shield; }
            protected set { shield = value; }
        }

        public int Agility { get; protected set; }
        public bool Alive { get; set; }
        public int Xp { get; protected set; }
        

        public Enemy(Player player, string type)
        {
            IsBoss = false;
            int lowHp = 20;
            int highHp = 35;
            Type = type;
            int lowStrength = 3;
            int highStrength = 7;
            if (type == "Axed goblin" || type =="Long footed muppet")
            {
                Agility = 20;
            }
            Alive = true;
            Random rand = new Random();
            switch (player.Level)
            {
                case 1:
                case 2:
                    Gold = rand.Next(20, 51);
                    break;
                case 3:
                case 4:
                    Gold = rand.Next(35, 71);
                    break;
                case 5:
                case 6:
                    Gold = rand.Next(45, 91);
                    break;
                case 7:
                    Gold = rand.Next(60, 111);
                    break;
                case 8:
                    Gold = rand.Next(70, 131);
                    break;
                case 9:
                    Gold = rand.Next(80, 151);
                    break;


            }
            
            

            
            


            double[,] healthSpanArray = new double[10, 2]
            {
                { lowHp-5, highHp-10},
                { lowHp*1.2, highHp*2},
                { lowHp*2, highHp*3.2},
                { lowHp*3, highHp*4.2},
                { lowHp*4.4, highHp*5.8},
                { lowHp*6, highHp*6.8},
                { lowHp*7, highHp*8.4},
                { lowHp*8, highHp*9},
                { lowHp*9, highHp*11},
                { lowHp*10, highHp*12}
            };
            
            switch (player.Level)
            {
                case 1:
                    Health = rand.Next((int)Math.Round(healthSpanArray[0, 0]), (int)Math.Round(healthSpanArray[0, 1]));
                    Strength = rand.Next(lowStrength,highStrength+1);
                    Xp = ((10 * player.Level) - player.Level+Strength);
                    break;
                case 2:
                    Health = rand.Next((int)healthSpanArray[1, 0], (int)healthSpanArray[1, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1)*1.4);
                    Xp = ((10 * player.Level) - player.Level + Strength);
                    break;
                case 3:
                    Health = rand.Next((int)healthSpanArray[2, 0], (int)healthSpanArray[2, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 1.9);
                    Xp = ((10 * player.Level) - player.Level + Strength);
                    break;
                case 4:
                    Health = rand.Next((int)healthSpanArray[3, 0], (int)healthSpanArray[3, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 2.1);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 5:
                    Health = rand.Next((int)healthSpanArray[4, 0], (int)healthSpanArray[4, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 2.5);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 6:
                    Health = rand.Next((int)healthSpanArray[5, 0], (int)healthSpanArray[5, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 2.8);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength*2);
                    break;
                case 7:
                    Health = rand.Next((int)healthSpanArray[6, 0], (int)healthSpanArray[6, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 3.5);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength*2);
                    break;
                case 8:
                    Health = rand.Next((int)healthSpanArray[7, 0], (int)healthSpanArray[7, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+1) * 3.8);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength * 2);
                    break;
                case 9:
                    Health = rand.Next((int)healthSpanArray[8, 0], (int)healthSpanArray[8, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength+2) * 4.5);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength * 3);
                    break;
               
            }

            if (type == "Axed goblin")
            {
                Shield = 25;
            }
            
            else
            {
                Shield = 0;
            }


        }

        public virtual string Attack(Player player)
        {
            Random rand = new Random();
            int randomisedAttack = rand.Next(1, 101);
            
            if (randomisedAttack > 16)
            {
                //Doing a normal attack
                return player.TakeDamage(NormalAttack(), true);
            }
            else
            {
                return player.TakeDamage(SpecialAttack(), true);
                //Doing a special attack
            }

        }

        private int NormalAttack()
        {
            return Strength;
        }

        private int SpecialAttack()
        {
            Random rand = new Random();
            if (IsBoss)
            {
                return Strength * 3 + rand.Next(5, 11);
            }
            else 
            {
                return (int)Math.Round(Strength * 1.5 + rand.Next(5, 11));
            }
        }

        public virtual void TakeDamage(StringBuilder textToPrint, int damage, bool lucky, int luckyDamage)
        {
            bool evaded = false;
                Random rand = new Random();
            if (rand.Next(1, 101) <= Agility)
            {
                textToPrint.AppendLine(@$"{ToString()} evaded your attack.");
                evaded = true;
            }
            else if (rand.Next(1, 101) <= Shield)
            {
                textToPrint.AppendLine(@$"{ToString()} used his shield to protect himself");
                evaded = true;
            }
            else
            {
                if (!lucky)
                {
                    Health -= (damage);
                    if (Health <= 0)
                    {
                        Alive = false;
                    }
                }
            }
            if (lucky && !evaded)
            {
                textToPrint.AppendLine($"CRITICAL HIT! You dealt {damage+luckyDamage} damage");
                Health -= (damage+luckyDamage);
                if (Health <= 0)
                {
                    Alive = false;
                }
            }
            else if (!evaded)
            {
                textToPrint.AppendLine($"You dealt {damage} damage to the {ToString()}.");
            }
           
        }

        public int DropGold()
        {
            return Gold;
        }
        public int GiveXp()
        {
            return Xp;
        }

        public override string ToString()
        {
            return Type;
        }
    }

}
