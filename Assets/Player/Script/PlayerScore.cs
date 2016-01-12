using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {
	// score attribute
	public int score;

	// other required scripts
	private PlayerLevel playerLevel;
	private PlayerAttack playerAttack;

	// initialize score attribute
	void Awake() {
		playerLevel = GetComponent<PlayerLevel> ();
		playerAttack = GetComponent<PlayerAttack> ();
	}
		
	void Update() {
		clampScore ();

		// check if player are elligible for level up
		if (score > playerLevel.getScoreLevel ())
			playerLevel.levelUP ();
	}

	// other methods
	void clampScore() { // clamp player's score so that the score is not negative
		if (score <= 0)
			score = 0;
	}

	public void increaseScore() {
		score += playerAttack.getAtk ();
	}

	public void decreaseScore(int amount) {
		score -= amount;
	}

	// setter & getter
	public void setScore(int amount) { score = amount; }

	public int getScore() { return score; }
}
