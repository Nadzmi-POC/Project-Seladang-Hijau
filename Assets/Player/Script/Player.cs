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

	// player's other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player characteristic var (public)

	// action variables
	private bool grounded, doubleJump;
	private bool facingLeft;

	void Awake() {
		// intialize player's attribute
		playerScore = GetComponent<PlayerScore> ();
		playerAttack = GetComponent<PlayerAttack> ();
		playerEnergy = GetComponent<PlayerEnergy> ();
		playerLevel = GetComponent<PlayerLevel> ();
	}

	void Start () {
		// other flag status
		grounded = false;
		facingLeft = true;
		doubleJump = false;

		// initialize enemy's attribute
		enemy = GameObject.FindGameObjectWithTag("Enemy");
	}

	void Update() { // logic update
		playerLevel.checkLevelUP (playerScore, playerEnergy, playerAttack);
	}

	void FixedUpdate () { // physic update
		action ();
		guiUpdate ();
	}

	void action() { // action function
		/* ------------------------ Movement ---------------------------- */
		float movement = Input.GetAxis ("Horizontal");

		// flip the sprite to facing left or right
		if (movement > 0 && facingLeft)
			flip ();
		else if (movement < 0 && !facingLeft)
			flip ();

		// move player
		transform.position += new Vector3 ((movement * speed), 0, 0);
		/* -------------------------- # ------------------------------ */

		/* ------------------------ jump ----------------------------- */
		groundChecker.position = new Vector3 (transform.position.x, groundChecker.position.y, 0);
		grounded = Physics2D.OverlapCircle (groundChecker.position, .02f, targetGround);

		if (grounded)
			doubleJump = false;

		if (Input.GetKeyDown (KeyCode.Space) && (grounded || !doubleJump)) {
			GetComponent<Rigidbody2D> ().AddForce (Vector2.up * jumpForce);

			if(!doubleJump && !grounded)
				doubleJump = true;
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false;

		if (playerAttack.getCanAttack()) {
			if(Input.GetKeyDown (KeyCode.Z)) {
				attacking = true;
				playerEnergy.energyDecrease (20); // player attempt to attack, energy will be decreased by 20
				
				if (attack.IsTouching(enemy.GetComponent<Collider2D> ())) // attack attempt successful, score will be increased by player atk
					playerScore.increaseScore(playerAttack.getAtk ());

				if (playerEnergy.getEnergy () < 20)
					playerAttack.setCanAttack (false);
			}
		} else {
			playerEnergy.isExhausted (playerAttack, energyGUI);
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
	/* ------------------------- # -----------------------------*/
}
