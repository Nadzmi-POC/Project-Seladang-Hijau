using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	// attack attribute
	private int atk;
	private bool canAttack;

	// initialize attack attribute
	void Awake() {
		atk = 1;
		canAttack = true;
	}

	// setter and getter
	public void setAtk(int amount) { atk = amount; }
	public void setCanAttack(bool tof) { canAttack = tof; }

	public int getAtk() { return atk; }
	public bool getCanAttack() { return canAttack; }
}
