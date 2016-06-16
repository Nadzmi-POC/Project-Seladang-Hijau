using UnityEngine;
using UnityEngine.UI;
using Assets.Enemy.Script;

public class Enemy : MonoBehaviour {
    // enemy's attribute
    public static EnemyAttribute enemyAttribute;

	// player's attribute
	GameObject player;

	// enemy's other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float jumpForce; // player characteristic var (public)
	public AudioClip hitSound;

	// action variables
	private bool facingLeft;
	private bool grounded;
	private bool doubleJump;

	private AudioSource source;

	void Awake() {
        // intialize enemy's attribute
        enemyAttribute = new EnemyAttribute();
        enemyAttribute.Score = 0;
        enemyAttribute.Level = 1;
        enemyAttribute.LevelScore = 10;
        enemyAttribute.Energy = 100;
        enemyAttribute.MaxEnergy = 100;
        enemyAttribute.Speed = 2;
        enemyAttribute.Attack = 1;
        enemyAttribute.CanAttack = true;

        // other flag status
        facingLeft = true; // enemy start in the scene facing left
		grounded = false;
		doubleJump = false;

		source = GetComponent<AudioSource> ();
	}

	void Start () {
        // initialize player's attributes
        player = GameObject.FindGameObjectWithTag("Player"); // find player's gamobject in the scene
	}

	void Update() { // logic update
        PlayerEnergyProvider.energyIncrease(1);
        EnemyLevelProvider.checkLevelUP(); // check whether the enemy is eligible to level up

        EnemyEnergyProvider.energyClamp();
        EnemyScoreProvider.clampScore();
        EnemyLevelProvider.levelClamp();
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
            PlayerScoreProvider.increaseScore(Player.playerAttribute.Attack);

			// effects lepas kena hit
			source.PlayOneShot (hitSound);

			if (Player.getFacingLeft ())
				GetComponent<Rigidbody2D> ().velocity = new Vector2(-150, 80);
			else
				GetComponent<Rigidbody2D> ().velocity = new Vector2(150, 80);
		} else if (gameObject.CompareTag ("RingBound")) {
            EnemyScoreProvider.decreaseScore(5);

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
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (player.transform.position.x, transform.position.y), (enemyAttribute.Speed / 2));
			else if (distance < 100) // distance < 100, enemy will chase player within it's sight
				transform.position = Vector2.MoveTowards (transform.position, new Vector2 (player.transform.position.x, transform.position.y), enemyAttribute.Speed);
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
				if (enemyAttribute.CanAttack) { // check if enemy are able to attack
					attacking = true; // enemy will attack
					EnemyEnergyProvider.energyDecrease (5); // enemy attempt to attack, energy will be decreased by 20

					if (enemyAttribute.Energy < 20) // if the last attack reduce the energy below the capcity
                        enemyAttribute.CanAttack = false;
				} else
                    EnemyEnergyProvider.isExhausted (energyGUI); // enemy exhausted, cannot attack until energy is replinished
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
		energyGUI.text = "ENERGY: " + enemyAttribute.EnergyPercent.ToString ("F0") + "%";
		scoreGUI.text = "SCORE: " + enemyAttribute.Score.ToString ();
		lvlGUI.text = "LEVEL: " + enemyAttribute.Level.ToString ();
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
