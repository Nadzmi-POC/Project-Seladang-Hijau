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
	Player playerAttribute;
	PlayerScore playerScore;
	PlayerAttack playerAttack;

	// enemy's other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player characteristic var (public)
	public AudioClip hitSound;

	// action variables
	private bool facingLeft;
	private bool grounded;
	private bool doubleJump;

	private AudioSource source;

	void Awake() {
		// intialize enemy's attribute
		enemyScore = GetComponent<EnemyScore> ();
		enemyAttack = GetComponent<EnemyAttack> ();
		enemyEnergy = GetComponent<EnemyEnergy> ();
		enemyLevel = GetComponent<EnemyLevel> ();

		// other flag status
		facingLeft = true; // enemy start in the scene facing left
		grounded = false;
		doubleJump = false;

		source = GetComponent<AudioSource> ();
	}

	void Start () {
		// initialize player's attributes
		player = GameObject.FindGameObjectWithTag("Player"); // find player's gamobject in the scene
		playerAttribute = player.GetComponent<Player> ();
		playerScore = player.GetComponent<PlayerScore> ();
		playerAttack = player.GetComponent<PlayerAttack> ();
	}

	void Update() { // logic update
		enemyLevel.checkLevelUP (enemyScore, enemyEnergy, enemyAttack); // check whether the enemy is eligible to level up
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	// trigger functions
	/* --------------------------- hit ------------------------------ */
	void OnTriggerEnter2D(Collider2D gameObject) { // trigger when enemy are being hit player's attacker
		if (gameObject.CompareTag("PlayerAttacker")) {
			// enemy attack attempt successful, enemy score will be increased by enemy atk
			playerScore.increaseScore (playerAttack.getAtk ());

			// effects lepas kena hit
			source.PlayOneShot (hitSound);

			if (playerAttribute.getFacingLeft ())
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-150, 80);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2(150, 80);
		} else if (gameObject.CompareTag ("RingBound")) {
			enemyScore.decreaseScore (5);

			transform.position = new Vector2 (0, 0);
		}
	}
	/* -------------------------------------------------------------- */

	// basic functions
	void action() { // action function
		float distance = Vector2.Distance (transform.position, player.transform.position); // calculate distance between enemy and player

		/* ------------------------ Movement ---------------------------- */
		float direction = transform.position.x - player.transform.position.x; // calculate the direction of player

		// if direction is (-ve), the enemy will flip to face the player and vice versa
		if (direction < 0 && facingLeft)
			flip ();
		else if (direction > 0 && !facingLeft)
			flip ();
		
		if (!(distance <= 20)) { // enemy akan berhenti bila distance <= 30
			if (distance >= 100) // distance >= 100, enemy will start looking for player by decreasing it's speed
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (player.transform.position.x, transform.position.y), (speed / 2));
			else if (distance < 100) // distance < 100, enemy will chase player within it's sight
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (player.transform.position.x, transform.position.y), speed);
		}
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		/*
		 * enemy will jump when near player and player is above him
		 * enemy will double jump if's it's current jumping do not reach the player
		 */
		// calculate distance between enemy and player in terms of y-axis
		float distanceY = transform.position.y - player.transform.position.y; // xleh pakai absolute value sbb nak enemy lompat bila player ada kat atas dia ja, bukan bawah dia
		float distanceX = Mathf.Abs(transform.position.x - player.transform.position.x);
		float playerVSpeed = player.GetComponent<Rigidbody2D>().velocity.y;

		groundChecker.position = new Vector3 (transform.position.x, groundChecker.position.y, 0); // update grounchecker's position
		grounded = Physics2D.OverlapCircle (groundChecker.position, .02f, targetGround); // check if the groundchecker overlap the ground

		if ((playerVSpeed > 50) || (distanceX <= 100)) {
			if ((distanceY *= -1) > 20) { // if the player is above enemy
				if (grounded || doubleJump) { // if enemy is currently grounded or can doublejump
					GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce); // enemy will jump or double jump
					doubleJump = !doubleJump; // change the status of doubleJump
				}
			}
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false; // by default, enemy is not attacking

		if (grounded) {
			if (distance <= 50) { // if enemy is near player, enemy will start to attack player
				if (enemyAttack.getCanAttack()) { // check if enemy are able to attack
					attacking = true; // enemy will attack
					enemyEnergy.energyDecrease (5); // enemy attempt to attack, energy will be decreased by 20

					if (enemyEnergy.getEnergy () < 20) // if the last attack reduce the energy below the capcity
						enemyAttack.setCanAttack (false);
				} else
					enemyEnergy.isExhausted (enemyAttack, energyGUI); // enemy exhausted, cannot attack until energy is replinished
			}
		}
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
		GetComponent<Animator>().SetBool("attack", attacking);
		GetComponent<Animator> ().SetBool ("grounded", grounded);
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

	public bool getFacingLeft() { return facingLeft; }
	/* ------------------------- # -----------------------------*/
}
