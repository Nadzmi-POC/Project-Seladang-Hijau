using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Enemy.Script
{
    public class EnemyAttribute
    {
        // attrivutes
        private int score;
        private int level, levelScore;
        private int attack, speed;
        private float energy, energyPercent, maxEnergy;
        private bool canAttack;

        // constructor
        public EnemyAttribute()
        {
            score = 0;
            level = 1;
            levelScore = 10;
            attack = 1;
            speed = 1;
            energy = 100;
            maxEnergy = 100;
            energyPercent = 100;
            canAttack = true;
        }

        // setter & getter
        public int Score { get; set; }
        public int Level { get; set; }
        public int LevelScore { get; set; }
        public int Attack { get; set; }
        public int Speed { get; set; }
        public bool CanAttack { get; set; }
        public float Energy { get; set; }
        public float MaxEnergy { get; set; }
        public float EnergyPercent
        {
            get
            {
                return ((Energy / MaxEnergy) * 100);
            }
        }
    }
}
