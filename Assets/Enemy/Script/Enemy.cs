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
	PlayerScore playerScore;

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
		playerScore = player.GetComponent<PlayerScore> ();
	}

	void Update() { // logic update
		enemyLevel.checkLevelUP (enemyScore, enemyEnergy, enemyAttack);
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	void action() { // action function
		/* ------------------------ Movement ---------------------------- */
		float distance = Vector2.Distance (transform.position, player.transform.position);

		if(distance >= 100)
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed);
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		if(groundChecker.GetComponent<BoxCollider2D>().IsTouching(GameObject.FindGameObjectWithTag("JumpBase").GetComponent<Collider2D> ()))
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);
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
	void flip() { // flip the enemy's sprite to left or right
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	/* ------------------------- # -----------------------------*/
}
