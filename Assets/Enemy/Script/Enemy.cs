using UnityEngine;
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
		// intialize enemy's attribute
		enemyScore = GetComponent<EnemyScore> ();
		enemyAttack = GetComponent<EnemyAttack> ();
		enemyEnergy = GetComponent<EnemyEnergy> ();
		enemyLevel = GetComponent<EnemyLevel> ();
	}

	void Start () {
		// other flag status
		facingLeft = true; // enemy start in the scene facing left

		// initialize player's attributes
		player = GameObject.FindGameObjectWithTag("Player"); // find player's gamobject in the scene
	}

	void Update() { // logic update
		enemyLevel.checkLevelUP (enemyScore, enemyEnergy, enemyAttack); // check whether the enemy is eligible to level up
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	void action() { // action function
		float distance = Vector2.Distance (transform.position, player.transform.position); // calculate distance between enemy and player

		/* ------------------------ Movement ---------------------------- */
		float direction = transform.position.x - player.transform.position.x; // calculate the direction of player

		// if direction is (-ve), the enemy will flip to face the player and vice versa
		if (direction < 0 && facingLeft)
			flip ();
		else if (direction > 0 && !facingLeft)
			flip ();

		if(distance >= 100) // distance >= 100, enemy will start looking for player by decreasing it's speed
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, (speed / 10));
		else if(distance < 100) // distance < 100, enemy will chase player within it's sight
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed);
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		if (GameObject.FindGameObjectWithTag ("JumpBase") != null) { // search for JumpBase in the scene (if any)
			if (groundChecker.GetComponent<BoxCollider2D> ().IsTouching (GameObject.FindGameObjectWithTag ("JumpBase").GetComponent<Collider2D> ()))
				GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce); // enemy jump when it hit the Jumpbase
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false;

		if (distance <= 50) { // if enemy is near player, enemy will start to attack player
			if (enemyAttack.getCanAttack()) { // check if enemy are able to attack
				attacking = true; // enemy will attack
				enemyEnergy.energyDecrease (20); // enemy attempt to attack, energy will be decreased by 20

				if (attack.IsTouching(player.GetComponent<Collider2D> ())) // attack attempt successful, score will be increased by enemy atk
					enemyScore.increaseScore(enemyAttack.getAtk ()); // increase enemy score by enemy' atk value

				if (enemyEnergy.getEnergy () < 20) // if the last attack reduce the energy below the capcity
					enemyAttack.setCanAttack (false);
			} else
				enemyEnergy.isExhausted (enemyAttack, energyGUI); // enemy exhausted, cannot attack until energy is replinished
		}
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
		GetComponent<Animator>().SetBool("attack", attacking);
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
