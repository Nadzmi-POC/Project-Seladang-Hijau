namespace Assets.Player.Script
{
    public class PlayerAttribute
    {
        // attrivutes
        private int score;
        private int level, levelScore;
        private int attack, speed;
        private float energy, energyPercent, maxEnergy;
        private bool canAttack;

        // constructor
        public PlayerAttribute()
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

        public PlayerAttribute(int score, int level, int levelScore, int attack, int speed, float energy, float maxEnergy, bool canAttack)
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
