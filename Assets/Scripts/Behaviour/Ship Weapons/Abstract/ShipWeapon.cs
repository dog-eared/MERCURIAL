using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon : MonoBehaviour {

	public string weaponName = "Ship Weapon";

	//[HideInInspector]
	public string factionName;

	protected ShipData _shipData;

	bool buttonBeingHeld = false;

	public virtual void FireButtonPressed() {
		Debug.Log("Pressed");
		buttonBeingHeld = true;
	}

	public virtual void FireButtonReleased() {
		Debug.Log("Release");
		buttonBeingHeld = false;
	}

	public virtual void GenerateBulletPool(string ownerTag) {
		Debug.Log("Generating bulletpool for " + ownerTag);
	}

}
