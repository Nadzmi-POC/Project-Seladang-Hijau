using UnityEngine;
using System.Collections;

public class EnemyLevelProvider : MonoBehaviour {
	// other methods
	public static void checkLevelUP() {
		if (Enemy.enemyAttribute.Score >= Enemy.enemyAttribute.LevelScore) // check if the player can level up or not
			levelUP ();
	}

    public static void levelClamp()
    {
        if (Enemy.enemyAttribute.Level >= 5)
            Enemy.enemyAttribute.Level = 5;
    }

    public static void levelUP() {// level up
        /*
        * if the score >= the score required to level up(lvlScore), the player will level up.
        * the lvlScore will be multiplied by 3
        */

        Enemy.enemyAttribute.Level++;
        Enemy.enemyAttribute.LevelScore *= 3;

		switch (Enemy.enemyAttribute.Level) {
            case 1:
                Enemy.enemyAttribute.MaxEnergy = 110;
                Enemy.enemyAttribute.Attack = 2;
                break;
            case 2:
                Enemy.enemyAttribute.MaxEnergy = 120;
                Enemy.enemyAttribute.Attack = 3;
                break;
            case 3:
                Enemy.enemyAttribute.MaxEnergy = 130;
                Enemy.enemyAttribute.Attack = 4;
                break;
            case 4:
                Enemy.enemyAttribute.MaxEnergy = 140;
                Enemy.enemyAttribute.Attack = 5;
                break;
            case 5:
                Enemy.enemyAttribute.MaxEnergy = 150;
                Enemy.enemyAttribute.Attack = 6;
                break;
        }
	}
}
