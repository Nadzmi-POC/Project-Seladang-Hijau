﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
	// enemy's attribute
	EnemyScore enemyScore;
	EnemyAttack enemyAttack;
	EnemyEnergy enemyEnergy;
	EnemyLevel enemyLevel;

	// player's attribute
	GameObject player;

	// enemy's other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player characteristic var (public)

	// action variables
	private bool facingLeft;

	void Awake() {
		// intialize player's attribute
		enemyScore = GetComponent<EnemyScore> ();
		enemyAttack = GetComponent<EnemyAttack> ();
		enemyEnergy = GetComponent<EnemyEnergy> ();
		enemyLevel = GetComponent<EnemyLevel> ();
	}

	void Start () {
		// other flag status
		facingLeft = true;

		// initialize player's attributes
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update() { // logic update
		enemyLevel.checkLevelUP (enemyScore, enemyEnergy, enemyAttack);
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	void action() { // action function
		float distance = Vector2.Distance (transform.position, player.transform.position);

		/* ------------------------ Movement ---------------------------- */
		float direction = transform.position.x - player.transform.position.x;

		if (direction < 0 && facingLeft)
			flip ();
		else if (direction > 0 && !facingLeft)
			flip ();

		if(distance >= 100)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, (speed / 10));
		else if(distance < 100)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed);
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		if (GameObject.FindGameObjectWithTag ("JumpBase") != null) {
			if (groundChecker.GetComponent<BoxCollider2D> ().IsTouching (GameObject.FindGameObjectWithTag ("JumpBase").GetComponent<Collider2D> ()))
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false;

		if (distance <= 50) {
			if (enemyAttack.getCanAttack()) {
				attacking = true;
				enemyEnergy.energyDecrease (20); // enemy attempt to attack, energy will be decreased by 20

				if (attack.IsTouching(player.GetComponent<Collider2D> ())) // enemy attempt successful, score will be increased by player atk
					enemyScore.increaseScore(enemyAttack.getAtk ());

				if (enemyEnergy.getEnergy () < 20)
					enemyAttack.setCanAttack (false);
			} else
				enemyEnergy.isExhausted (enemyAttack, energyGUI);
		}
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
		// GetComponent<Animator>().SetBool("attack", attacking);
		/* --------------------------- # --------------------------------- */
	}

	/* -------------------- GUI update ------------------------- */
	void guiUpdate() { // update the ui
		energyGUI.text = "ENERGY: " + enemyEnergy.getEnergyPercent().ToString ("F0") + "%";
		scoreGUI.text = "SCORE: " + enemyScore.getScore().ToString ();
		lvlGUI.text = "LEVEL: " + enemyLevel.getLevel().ToString ();
	}
	/* -------------------------- # --------------------------- */

	/* -------------------- Other methods ---------------------- */
	void flip() { // flip the enemy's sprite to left or right
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	/* ------------------------- # -----------------------------*/
}
