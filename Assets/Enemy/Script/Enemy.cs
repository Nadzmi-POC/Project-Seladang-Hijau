using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Text energyGUI, scoreGUI, lvlGUI;

	private int score, lvl;
	private float en, maxEn, enPercent;

	void Start () {
		score = 0;
		lvl = 1;
		en = 100f;
		maxEn = 100f;
		enPercent = (en / maxEn) * 100;
	}

	void FixedUpdate () {
		action ();
	}

	void action() {
		/* ---------------------- Update attribute -------------------- */
		enPercent = (en / maxEn) * 100;
		/* ------------------------------------------------------------ */

		/* --------------------- Update GUI ------------------- */
		energyGUI.text = enPercent + "% :ENERGY";
		scoreGUI.text = score + " :SCORE";
		lvlGUI.text = lvl + " :LEVEL";
		/* ----------------------------------------------------- */
	}

	void scoreDecrease(int amount) {
		score -= amount;
	}
}
