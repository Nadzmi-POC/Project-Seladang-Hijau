using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {
	// enemy's attribute
	EnemyAttack enemyAttack;
	EnemyEnergy enemyEnergy;
	EnemyLevel enemyLevel;
	EnemyScore enemyScore;

	// player's attribute
	PlayerScore playerScore;

	// other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player characteristic var (public)

	// action variables
	private bool grounded, doubleJump;
	private bool facingLeft;

	// player's object
	GameObject player;

	void Start() {
		// intialize player's attribute
		enemyScore = GetComponent<EnemyScore> ();
		enemyAttack = GetComponent<EnemyAttack> ();
		enemyEnergy = GetComponent<EnemyEnergy> ();
		enemyLevel = GetComponent<EnemyLevel> ();

		// other flag status
		grounded = false;
		facingLeft = true;
		doubleJump = false;

		// player's object initialization
		player = GameObject.FindGameObjectWithTag("Player");
		playerScore = player.GetComponent<PlayerScore> ();
	}

	void FixedUpdate() {
		action ();
		guiUpdate ();
	}

	void action() {
		/* ------------------------ Movement ---------------------------- */
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
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
	void flip() { // flip the player's sprite to left or right
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	/* ------------------------- # -----------------------------*/
}
