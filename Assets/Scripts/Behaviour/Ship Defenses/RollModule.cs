using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollModule : ShipDefense {

	/* ROLL MODULE

	Allows a ship to roll, moving them directly left or right from their current
	position.


	*/

	public float rollSpeed = 0.3f;

	void Awake() {
		defenseName = "Roll Module : " + direction;
	}

	void FixedUpdate() {
		if (buttonBeingHeld && direction == "left") {
			transform.Translate(Vector3.right * -1 * rollSpeed * Time.deltaTime);
		} else if (buttonBeingHeld) {
			transform.Translate(Vector3.right * rollSpeed * Time.deltaTime);
		}
	}

}
