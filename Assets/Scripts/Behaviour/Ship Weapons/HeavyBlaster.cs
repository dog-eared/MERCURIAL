using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBlaster : ShipWeapon {

	public string weaponName = "Heavy Blaster";

	override public void FireButtonPressed() {
		Debug.Log(weaponName + " pressed");
	}

	override public void FireButtonReleased() {
		Debug.Log(weaponName + " released");
	}

}
