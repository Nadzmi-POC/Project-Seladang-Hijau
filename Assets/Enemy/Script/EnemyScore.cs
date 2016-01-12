using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {
	// score attribute
	public int score;

	// other required scripts
	private EnemyLevel enemyLevel;
	private EnemyAttack enemyAttack;

	// initialize score attribute
	void Awake() {
		enemyLevel = GetComponent<EnemyLevel> ();
		enemyAttack = GetComponent<EnemyAttack> ();
	}
		
	void Update() {
		clampScore ();

		// check if player are elligible for level up
		if (score > enemyLevel.getScoreLevel ())
			enemyLevel.levelUP ();
	}

	// other methods
	void clampScore() { // clamp enemy's score so that the score is not negative
		if (score <= 0)
			score = 0;
	}

	public void increaseScore() {
		score += enemyAttack.getAtk ();
	}

	public void decreaseScore(int amount) {
		score -= amount;
	}

	// setter & getter
	public void setScore(int amount) { score = amount; }

	public int getScore() { return score; }
}
