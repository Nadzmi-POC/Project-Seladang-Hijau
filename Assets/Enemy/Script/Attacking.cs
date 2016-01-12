using UnityEngine;
using System.Collections;

public class Attacking : MonoBehaviour {
	EnemyScore enemyScore;
	EnemyAttack enemyAttack;
	EnemyEnergy enemyEnergy;

	PlayerScore playerScore;

	private bool attacking;

	void Awake() {
		enemyAttack = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyAttack> ();
		enemyScore = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyScore> ();
		enemyEnergy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<EnemyEnergy> ();
		playerScore = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScore> ();
	}

	void Update() {
		GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Animator> ().SetBool ("attack", attacking);
	}

	void OnTriggerEnter2D(Collider2D gameObject) {
		if (gameObject.tag == "Player") {
			if (enemyAttack.getCanAttack ()) {
				attacking = true;

				enemyEnergy.energyDecrease (20);
				enemyScore.increaseScore ();
				// playerScore.decreaseScore (enemyAttack.getAtk ());
			} else
				attacking = false;

			if (enemyEnergy.getEnergy () < 20)
				enemyEnergy.isExhausted ();
		} else
			attacking = false;
	}
}
