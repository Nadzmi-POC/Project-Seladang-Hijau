using UnityEngine;
using System.Collections;

public class EnemyScoreProvider : MonoBehaviour {
	public static void clampScore() { // clamp player's score so that the score is not negative
		if (Enemy.enemyAttribute.Score <= 0)
            Enemy.enemyAttribute.Score = 0;
	}

	public static void increaseScore(int amount) { // increase the score by the amount
        Enemy.enemyAttribute.Score += amount;
	}

	public static void decreaseScore(int amount) { // decrease the score by the amount
        Enemy.enemyAttribute.Score -= amount;
	}
}
