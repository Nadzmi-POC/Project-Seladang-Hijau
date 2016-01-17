using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	// player's attribute
	PlayerScore playerScore;
	PlayerAttack playerAttack;
	PlayerEnergy playerEnergy;
	PlayerLevel playerLevel;

	// enemy's attribute
	GameObject enemy;
	Enemy enemyAttribute;
	EnemyScore enemyScore;
	EnemyAttack enemyAttack;

	// player's other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player characteristic var (public)
	public AudioClip attackSound;

	// action variables
	private bool facingLeft;
	private bool grounded, doubleJump;

	private AudioSource source;

	void Awake() {
		// intialize player's attribute
		playerScore = GetComponent<PlayerScore> ();
		playerAttack = GetComponent<PlayerAttack> ();
		playerEnergy = GetComponent<PlayerEnergy> ();
		playerLevel = GetComponent<PlayerLevel> ();

		// other flag status
		grounded = false;
		facingLeft = true;
		doubleJump = false;

		source = GetComponent<AudioSource> ();
	}

	void Start () {
		// initialize enemy's attribute
		enemy = GameObject.FindGameObjectWithTag("Enemy"); // find enemy gameobject int the scene
		enemyAttribute = enemy.GetComponent<Enemy> ();
		enemyScore = enemy.GetComponent<EnemyScore> ();
		enemyAttack = enemy.GetComponent<EnemyAttack> ();
	}

	void Update() { // logic update
		playerLevel.checkLevelUP (playerScore, playerEnergy, playerAttack); // check whether player are eligible for level up
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	// trigger functions
	/* --------------------------- hit ------------------------------ */
	void OnTriggerEnter2D(Collider2D gameObject) { // trigger when enemy are being hit player's attacker
		if (gameObject.CompareTag("EnemyAttacker")) {
			// player attack attempt successful, player score will be increased by player atk
			enemyScore.increaseScore(enemyAttack.getAtk ());

			if (enemyAttribute.getFacingLeft ())
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-100, 80);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2(100, 80);
		}
	}
	/* -------------------------------------------------------------- */

	// basic functions
	void action() { // action function
		/* ------------------------ Movement ---------------------------- */
		float movement = Input.GetAxis ("Horizontal"); // get input axis from player

		// flip the sprite to facing left or right
		if (movement > 0 && facingLeft)
			flip ();
		else if (movement < 0 && !facingLeft)
			flip ();

		// move player according to the input axis
		transform.position += new Vector3 ((movement * speed), 0, 0);
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		groundChecker.position = new Vector3 (transform.position.x, groundChecker.position.y, 0); // update grounchecker's position
		grounded = Physics2D.OverlapCircle (groundChecker.position, .02f, targetGround); // check if the groundchecker overlap the ground

		if (grounded) // player currently grounded, player cannot double jump
			doubleJump = false; // player cannot do doublejump

		if (Input.GetKeyDown (KeyCode.Space) && (grounded || !doubleJump)) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce); // player can jump or double jump

			if(!doubleJump && !grounded) // player are not on the ground and not double jump yet, player can double jump
				doubleJump = true;
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false;

		if (grounded) {
			if (playerAttack.getCanAttack()) { // check eligibility for the player to attack
				if(Input.GetKeyDown (KeyCode.Z)) { // get player's input
					attacking = true; // player will attack
					playerEnergy.energyDecrease (20); // player attempt to attack, energy will be decreased by 20

					source.PlayOneShot (attackSound);

					if (playerEnergy.getEnergy () < 20) // if player's energy below the capacity, player cannot attack
						playerAttack.setCanAttack (false);
				}
			} else
				playerEnergy.isExhausted (playerAttack, energyGUI); // player is exhausted, wait until energy is 100% replinished to attack again
		}
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
		GetComponent<Animator> ().SetFloat ("movement", Mathf.Abs(movement));
		GetComponent<Animator> ().SetBool ("grounded", grounded);
		GetComponent<Animator> ().SetBool ("attack", attacking);
		GetComponent<Animator> ().SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);
		/* --------------------------- # --------------------------------- */
	}

	/* -------------------- GUI update ------------------------- */
	void guiUpdate() { // update the ui
		energyGUI.text = "ENERGY: " + playerEnergy.getEnergyPercent().ToString ("F0") + "%";
		scoreGUI.text = "SCORE: " + playerScore.getScore().ToString ();
		lvlGUI.text = "LEVEL: " + playerLevel.getLevel().ToString ();
	}
	/* -------------------------- # --------------------------- */

	/* -------------------- Other methods ---------------------- */
	void flip() { // flip the player's sprite to left or right
		facingLeft = !facingLeft;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool getFacingLeft() { return facingLeft; }
	/* ------------------------- # -----------------------------*/
}
