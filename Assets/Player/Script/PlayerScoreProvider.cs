using UnityEngine;
using System.Collections;

public class PlayerScoreProvider {
	// other methods
	public static void clampScore() { // clamp player's score so that the score is not negative
		if (Player.playerAttribute.Score <= 0)
			Player.playerAttribute.Score = 0;
	}

	public static void increaseScore(int amount) { // increase the score by the amount
        Player.playerAttribute.Score += amount;
	}

	public static void decreaseScore(int amount) { // decrease the score by the amount
        Player.playerAttribute.Score -= amount;
	}
}
