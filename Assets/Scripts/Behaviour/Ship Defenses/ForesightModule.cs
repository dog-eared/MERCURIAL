using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForesightModule : ShipDefense {

	/* FORESIGHT MODULE

	While button is held, controls

	*/
	public float currentDist = 0;
	public float foresightSpeed = 2f;
	public float foresightReverseMultiplier;


	public float foresightDistance = 3.5f;

	public Vector2 foresightLocation = Vector2.up;

	void Awake() {
		defenseName = "Foresight Module : " + direction;
	}

	void FixedUpdate() {
		if (buttonBeingHeld) {
			currentDist += foresightSpeed * Time.deltaTime;
		} else {
			currentDist -= foresightSpeed * Time.deltaTime * foresightReverseMultiplier;
		}
		foresightLocation = transform.up * Mathf.Lerp(0, foresightDistance, currentDist);
		currentDist = Mathf.Clamp(currentDist, 0, foresightDistance);
	}


}
