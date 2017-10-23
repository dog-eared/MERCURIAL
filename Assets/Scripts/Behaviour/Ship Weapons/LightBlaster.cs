using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlaster : ShipWeapon {

	public string weaponName = "Light Blaster";

	public float rateOfFire = 1f;
	float timeSinceLastShot = 0f;


	override public void FireButtonPressed() {
		Debug.Log(weaponName + " pressed");
	}

	override public void FireButtonReleased() {
		Debug.Log(weaponName + " released");
	}

}
