using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerEnergyProvider {
	// other methods
	public static void isExhausted(Text energyGUI) { // is currently exhausted
		Player.playerAttribute.Energy += 0.25f;

		if (Player.playerAttribute.EnergyPercent < 100) {
			energyGUI.color = Color.red;
			Player.playerAttribute.CanAttack = false;
		} else {
			energyGUI.color = Color.white;
            Player.playerAttribute.CanAttack = true;
		}
	}

	public static void energyDecrease(float amount) // decrease energy
    { 
		Player.playerAttribute.Energy -= amount;
	}

    public static void energyIncrease(float amount) // increase energy
    {
        Player.playerAttribute.Energy += amount;
    }

	public static void energyClamp() { // prevent the energy meter from going below or above max or min energy(0)
		if (Player.playerAttribute.Energy >= Player.playerAttribute.MaxEnergy)
            Player.playerAttribute.Energy = Player.playerAttribute.MaxEnergy;
		if(Player.playerAttribute.Energy <= 0)
            Player.playerAttribute.Energy = 0;
	}
}
