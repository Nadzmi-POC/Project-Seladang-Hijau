using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {
	// level attributes
	private int level, scoreLevel;

	void Awake() {
		level = 1;
		scoreLevel = 10;
	}

	// other methods
	public void checkLevelUP(PlayerScore playerScore, PlayerEnergy playerEnergy, PlayerAttack playerAttack) {
		if (playerScore.getScore () >= scoreLevel) // check if the player can level up or not
			levelUP (playerEnergy, playerAttack);
	}

	public void levelUP(PlayerEnergy playerEnergy, PlayerAttack playerAttack) { // level up
		/*
		 * if the score >= the score required to level up(lvlScore), the player will level up.
		 * the lvlScore will be multiplied by 3
		 */

		level++;
		scoreLevel *= 3;

		switch (level) {
		case 1:
			playerEnergy.setMaxEnergy (110);
			playerAttack.setAtk (2);
			break;
		case 2:
			playerEnergy.setMaxEnergy (120);
			playerAttack.setAtk (3);
			break;
		case 3:
			playerEnergy.setMaxEnergy (130);
			playerAttack.setAtk (4);
			break;
		case 4:
			playerEnergy.setMaxEnergy (140);
			playerAttack.setAtk (5);
			break;
		case 5:
			playerEnergy.setMaxEnergy (150);
			playerAttack.setAtk (6);
			break;
		}
	}

	// setter & getter
	public void setLevel(int amount) { level = amount; }
	public void setScoreLevel(int amount) { scoreLevel = amount; }

	public int getLevel() { return level; }
	public int getScoreLevel() { return scoreLevel; }
}
