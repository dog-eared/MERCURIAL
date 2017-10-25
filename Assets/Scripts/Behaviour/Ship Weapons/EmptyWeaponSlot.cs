using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWeaponSlot : ShipWeapon {

	void Awake() {
		weaponName = "Empty Slot";
	}

	override public void FireButtonPressed() {
	}

	override public void FireButtonReleased() {
	}

}
