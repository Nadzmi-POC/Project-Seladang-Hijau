﻿using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {
	// score attribute
	private int score;

	void Awake() {
		score = 0;
	}
		
	void Update() {
		clampScore ();
	}

	// other methods
	void clampScore() { // clamp player's score so that the score is not negative
		if (score <= 0)
			score = 0;
	}

	public void increaseScore(int amount) {
		score += amount;
	}

	public void decreaseScore(int amount) {
		score -= amount;
	}

	// setter & getter
	public void setScore(int amount) { score = amount; }

	public int getScore() { return score; }
}
