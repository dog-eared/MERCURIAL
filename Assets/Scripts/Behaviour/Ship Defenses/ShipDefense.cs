using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDefense : MonoBehaviour {

	/* SHIP defensive

	Abstract class for ship defenses.

	*/

	public string direction = "left";

	public string defenseName = "Defense";

	public bool buttonBeingHeld = false;

	public virtual void DefenseButtonPressed() {
		Debug.Log(defenseName + "Defense Pressed");
		buttonBeingHeld = true;
	}

	public virtual void DefenseButtonReleased() {
		Debug.Log(defenseName + "Defense Release");
		buttonBeingHeld = false;
	}

	public virtual void DefenseRotateLeft() {
		if (buttonBeingHeld) {
			Debug.Log(defenseName + "D + R left");
		}
	}

	public virtual void DefenseRotateRight() {
		if (buttonBeingHeld) {
			Debug.Log(defenseName + "D + R right");
		}
	}

}
