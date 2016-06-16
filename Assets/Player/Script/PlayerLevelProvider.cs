using UnityEngine;
using System.Collections;

public class PlayerLevelprovider {
	// other methods
	public static void checkLevelUP() {
		if (Player.playerAttribute.Score >= Player.playerAttribute.LevelScore) // check if the player can level up or not
			levelUP ();
	}

    public static void levelClamp()
    {
        if (Player.playerAttribute.Level >= 5)
            Player.playerAttribute.Level = 5;
    }

	public static void levelUP() { // level up
		/*
		 * if the score >= the score required to level up(lvlScore), the player will level up.
		 * the lvlScore will be multiplied by 3
		 */

		Player.playerAttribute.Level++;
		Player.playerAttribute.LevelScore *= 3;

		switch (Player.playerAttribute.Level) {
		case 1:
            Player.playerAttribute.MaxEnergy = 110;
            Player.playerAttribute.Attack = 2;
			break;
		case 2:
            Player.playerAttribute.MaxEnergy = 120;
            Player.playerAttribute.Attack = 3;
            break;
        case 3:
            Player.playerAttribute.MaxEnergy = 130;
            Player.playerAttribute.Attack = 4;
            break;
        case 4:
            Player.playerAttribute.MaxEnergy = 140;
            Player.playerAttribute.Attack = 5;
            break;
        case 5:
            Player.playerAttribute.MaxEnergy = 150;
            Player.playerAttribute.Attack = 6;
            break;
        }
	}
}
