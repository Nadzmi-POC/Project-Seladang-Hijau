using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
	// attack attribute
	public int atk;
	public bool canAttack;

	// initialize attack attribute
	void Awake() {
		canAttack = true;
	}

	// setter and getter
	public void setAtk(int amount) { atk = amount; }
	public void setCanAttack(bool tof) { canAttack = tof; }

	public int getAtk() { return atk; }
	public bool getCanAttack() { return canAttack; }
}
