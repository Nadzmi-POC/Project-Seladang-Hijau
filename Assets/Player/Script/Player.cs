using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
	// other components
	public LayerMask targetGround;
	public Transform groundChecker;
	public Collider2D attack;
	public Text energyGUI, scoreGUI, lvlGUI;

	public float speed, jumpForce; // player info var (public)
	
	// player info variables
	private float en, maxEn, enPercent;
	private int atk;
	private int score, lvl, lvlScore; // lvlScore = scroe kena dpt utk levelup

	// other action variables
	private bool grounded, doubleJump;
	private bool facingLeft;
	private bool canAttack;
	private bool lvlUP;

	// other object variables
	private Collider2D enemy;

	void Start () {
		// player basic info :
		atk = 1;
		score = 0;
		lvl = 1;
		lvlScore = 10;
		en = 100;
		maxEn = 100;
		enPercent = (en / maxEn) * 100;

		// other flag status
		grounded = false;
		facingLeft = true;
		canAttack = true;
		lvlUP = false;
		doubleJump = false;

		// other object status
		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Collider2D> ();
	}

	void FixedUpdate () {
		action ();
		checkLvlUp ();
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
			GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, jumpForce));

			if(!doubleJump && !grounded)
				doubleJump = true;
		}
		/* ------------------------- # --------------------------- */

		/* ----------------------- attack ------------------------------ */
		bool attacking = false;

		if (canAttack) {
			if(Input.GetKeyDown (KeyCode.Z)) {
				attacking = true;
				en -= 20; // if the player attempt to attack, their energy will be decreased by 20
				
				if (attack.IsTouching(enemy)) { // if the attack attempt successful, their score will be increased by atk
					score += atk;
				}

				if (en < 20) 
					canAttack = false;
			}
		} else {
			en += 0.25f;

			if (enPercent < 100) 
				canAttack = false;
			else
				canAttack = true;
		}

		en++;
		if (en >= maxEn)
			en = maxEn;
		if(en <= 0)
			en = 0;
		enPercent = (en / maxEn) * 100;
		/* ---------------------- # ----------------------------- */

		/* ---------------------- update animator ------------------- */
		GetComponent<Animator> ().SetFloat ("movement", Mathf.Abs(movement));
		GetComponent<Animator> ().SetBool ("grounded", grounded);
		GetComponent<Animator> ().SetBool ("attack", attacking);
		GetComponent<Animator> ().SetFloat ("vSpeed", GetComponent<Rigidbody2D> ().velocity.y);
		/* --------------------------- # --------------------------------- */
	}

	void checkLvlUp() { // check if player has leveled up or not
		/*
		 * if the score >= the score required to level up(lvlScore), the player will level up.
		 * the lvlScore will be multiplied by 3
		 */
		if (score >= lvlScore) {
			lvlUP = true;
			lvlScore *= 3;
			lvl++;

			switch (lvl) { // increase certain stat based on the level that the player has increased
			case 1:
				atk = 1;
				maxEn += 20;
				break;
			case 2:
				atk = 2;
				maxEn += 20;
				break;
			case 3:
				atk = 3;
				maxEn += 20;
				break;
			case 4:
				atk = 4;
				maxEn += 20;
				break;
			case 5:
				atk = 5;
				maxEn += 20;
				break;
			}
		} else
			lvlUP = false;
	}

	/* -------------------- GUI update ------------------------- */
	void guiUpdate() { // update the ui
		energyGUI.text = "ENERGY: " + enPercent.ToString ("F0") + "%";
		scoreGUI.text = "SCORE: " + score.ToString ();
		lvlGUI.text = "LEVEL: " + lvl.ToString ();
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
