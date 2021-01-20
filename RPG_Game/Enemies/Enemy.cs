using System;
using System.Collections.Generic;
using System.Text;
using RPG_Game.Gamer;
namespace RPG_Game.Enemies
{
    abstract class Enemy : IEnemy
    {
        public string Type { get; private set; }
        public int Health { get; private set; }
        public int Strength { get; private set; }
        public int Gold { get; private set; }
        public int Agility { get; private set; }
        public bool Alive { get; private set; }
        public int Xp { get; private set; }

        public Enemy(Player player)
        {
            int lowHp = 20;
            int highHp = 35;
            int lowStrength = 3;
            int highStrength = 7;
            
            double[,] healthSpanArray = new double[10, 2]
            {
                { lowHp, highHp},
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
            Random rand = new Random();
            
            switch (player.Level)
            {
                case 1:
                    Health = rand.Next((int)Math.Round(healthSpanArray[0, 0]), (int)Math.Round(healthSpanArray[0, 1]));
                    Strength = rand.Next(lowStrength,highStrength);
                    Xp = ((10 * player.Level) - player.Level+Strength);
                    break;
                case 2:
                    Health = rand.Next((int)healthSpanArray[1, 0], (int)healthSpanArray[1, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength)*1.4);
                    Xp = ((10 * player.Level) - player.Level + Strength);
                    break;
                case 3:
                    Health = rand.Next((int)healthSpanArray[2, 0], (int)healthSpanArray[2, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 1.9);
                    Xp = ((10 * player.Level) - player.Level + Strength);
                    break;
                case 4:
                    Health = rand.Next((int)healthSpanArray[3, 0], (int)healthSpanArray[3, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 2.1);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 5:
                    Health = rand.Next((int)healthSpanArray[4, 0], (int)healthSpanArray[4, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 2.5);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 6:
                    Health = rand.Next((int)healthSpanArray[5, 0], (int)healthSpanArray[5, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 2.8);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 7:
                    Health = rand.Next((int)healthSpanArray[6, 0], (int)healthSpanArray[6, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 3.5);
                    Xp = ((10 * player.Level) - player.Level + Gold + Strength);
                    break;
                case 8:
                    Health = rand.Next((int)healthSpanArray[7, 0], (int)healthSpanArray[7, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 3.8);
                    Xp = ((10 * player.Level) - player.Level + Gold*2);
                    break;
                case 9:
                    Health = rand.Next((int)healthSpanArray[8, 0], (int)healthSpanArray[8, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 4.5);
                    Xp = ((10 * player.Level) - player.Level + Gold*2);
                    break;
                case 10:
                    Health = rand.Next((int)healthSpanArray[9, 0], (int)healthSpanArray[9, 1]);
                    Strength = (int)Math.Round(rand.Next(lowStrength, highStrength) * 4.9);
                    break;
            }
            
            Agility = 5;
            Alive = true;
            
            Gold = rand.Next(5, 50);
            

        }

        public virtual string Attack(Player player)
        {

            int damage = Strength;
            return player.TakeDamage(damage, true);
            
        }

        public virtual void TakeDamage(StringBuilder textToReturn,int damage, bool lucky)
        {
            bool evaded = false;
                Random rand = new Random();
            if (rand.Next(1, 101) <= Agility)
            {
                textToReturn.AppendLine("The enemy evaded your attack.");
                evaded = true;
            }
            else
            {
                Health -= damage;
                if (Health <= 0)
                {
                    Alive = false;
                }
            }
            if (lucky && !evaded)
            {
                textToReturn.AppendLine($"CRITICAL HIT! You dealt {damage} damage");
            }
            else if (!evaded)
            {
                textToReturn.AppendLine($"You dealt {damage} damage to the monster.");
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


    }

}
