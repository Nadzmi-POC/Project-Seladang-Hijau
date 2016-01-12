using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {
	// level attributes
	public int level, scoreLevel;

	// other reqiured scripts
	private PlayerAttack playerAttack;
	private PlayerEnergy playerEnergy;
	private PlayerScore playerScore;

	// initialize level attribute
	void Awake() {
		playerAttack = GetComponent<PlayerAttack> ();
		playerEnergy = GetComponent<PlayerEnergy> ();
		playerScore = GetComponent<PlayerScore> ();
	}

	// update current level
	void Update() {
		if (playerScore.getScore () >= scoreLevel) // check if the player can level up or not
			levelUP ();
	}

	// other methods
	public void levelUP() { // level up
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
