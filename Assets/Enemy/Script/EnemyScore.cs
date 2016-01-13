using UnityEngine;
using System.Collections;

public class EnemyScore : MonoBehaviour {
	// score attribute
	public int score;

	void Update() {
		clampScore ();
	}

	// other methods
	public void clampScore() { // clamp player's score so that the score is not negative
		if (score <= 0)
			score = 0;
	}

	public void increaseScore(EnemyAttack enemyattack) {
		score += enemyattack.getAtk ();
	}

	public void decreaseScore(int amount) {
		score -= amount;
	}

	// setter & getter
	public void setScore(int amount) { score = amount; }

	public int getScore() { return score; }
}
