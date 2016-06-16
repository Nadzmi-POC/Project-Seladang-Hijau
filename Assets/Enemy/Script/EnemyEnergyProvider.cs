using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyEnergyProvider : MonoBehaviour {
	// other methods
	public static void isExhausted(Text energyGUI) { // is currently exhausted
		Enemy.enemyAttribute.Energy += 0.25f;

		if (Enemy.enemyAttribute.EnergyPercent < 100) {
			energyGUI.color = Color.red;
            Enemy.enemyAttribute.CanAttack = false;
		} else {
			energyGUI.color = Color.white;
            Enemy.enemyAttribute.CanAttack = true;
        }
	}

	public static void energyDecrease(float amount) // decrease energy
    { 
        Enemy.enemyAttribute.Energy -= amount;
	}

    public static void energyIncrease(float amount) // increase energy
    { 
        Enemy.enemyAttribute.Energy += amount;
    }

    public static void energyClamp() { // prevent the energy meter from going below or above max or min energy(0)
		if (Enemy.enemyAttribute.Energy >= Enemy.enemyAttribute.MaxEnergy)
            Enemy.enemyAttribute.Energy = Enemy.enemyAttribute.MaxEnergy;
		if(Enemy.enemyAttribute.Energy <= 0)
            Enemy.enemyAttribute.Energy = 0;
	}
}
