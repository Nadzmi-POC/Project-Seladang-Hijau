using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergy : MonoBehaviour {
	// energy attribute
	private float energy, maxEnergy, energyPercent;

	// initialize energy attributes
	void Awake() {
		energy = 100;
		maxEnergy = 100;
		energyPercent = 100;
	}

	// update energy percentage value
	void FixedUpdate() {
		energy++;
		energyClamp ();
		energyPercent = (energy / maxEnergy) * 100;
	}

	// other methods
	public void isExhausted(PlayerAttack playerAttack, Text energyGUI) { // is currently exhausted
		energy += 0.25f;

		if (energyPercent < 100) {
			energyGUI.color = Color.red;
			playerAttack.setCanAttack (false);
		} else {
			energyGUI.color = Color.white;
			playerAttack.setCanAttack (true);
		}
	}

	public void energyDecrease(float amount) { // decrease energy
		energy -= amount;
	}

	public void energyClamp() { // prevent the energy meter from going below or above max or min energy(0)
		if (energy >= maxEnergy)
			energy = maxEnergy;
		if(energy <= 0)
			energy = 0;
	}

	// setter & getter
	public void setEnergy(float amount) { energy = amount; }
	public void setMaxEnergy(float amount) { maxEnergy = amount; }

	public float getEnergy() { return energy; }
	public float getMaxEnergy() { return maxEnergy; }
	public float getEnergyPercent() { return energyPercent; }
}
