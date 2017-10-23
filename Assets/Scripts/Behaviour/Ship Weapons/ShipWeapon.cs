using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipWeapon : MonoBehaviour {

	public virtual void FireButtonPressed() {
		Debug.Log("Pressed");
	}

	public virtual void FireButtonReleased() {
		Debug.Log("Release");
	}

}
