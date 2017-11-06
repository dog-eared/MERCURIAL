using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon : MonoBehaviour {

	public string weaponName = "Ship Weapon";

	bool buttonBeingHeld = false;

	public virtual void FireButtonPressed() {
		Debug.Log("Pressed");
		buttonBeingHeld = true;
	}

	public virtual void FireButtonReleased() {
		Debug.Log("Release");
		buttonBeingHeld = false;
	}

}
