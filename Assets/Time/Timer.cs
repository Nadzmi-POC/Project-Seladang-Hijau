using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Timer : MonoBehaviour {
	public float totalTime; // total time taken for the game to end

	private float timeToEnd; // current time taken for the game to end

	void Start() {
		timeToEnd = totalTime;
	}

	void FixedUpdate () {
		timeToEnd -= 0.01f;

		if (timeToEnd <= 0) {
			timeToEnd = 0;

			GetComponent<Text> ().text = "GAME OVER";
		} else {
			GetComponent<Text> ().text = timeToEnd.ToString ("F0"); // output the time taken to end the game in ui
		}
	}

	public float getTimeToEnd() { return timeToEnd; }
}
