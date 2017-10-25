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
		Debug.Log("Defense Pressed");
		buttonBeingHeld = true;
	}

	public virtual void DefenseButtonReleased() {
		Debug.Log("Defense Release");
		buttonBeingHeld = false;
	}

}
