using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipDefense : MonoBehaviour {

	/* SHIP defensive

	Abstract class for ship defenses.

	*/

	public string defenseName = "Defense";

	public void ActivateDefense(bool toRight, float force) {
		Debug.Log("Defense: " + defenseName + " triggered");
		Debug.Log("Fired right: " + toRight + "with force: " + force);
	}

}
