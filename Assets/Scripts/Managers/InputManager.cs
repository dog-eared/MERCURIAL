using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	/*
		INPUT MANAGER

		--Handles input relating to game state and systems (ie opening GUI, debug)
		--Handles universal ship input -- stuff like comms button, board, change
		target, etc.
		--Handles player input that is ship specific -- movement handling, etc.

	*/

	public GameObject playerObject;
	public GameObject playerShip;

	SystemManager systemManager;

	InertialessMovement inertialessMovement;

	void Awake () {
		playerObject = GameObject.FindWithTag("PlayerObject");
		playerShip = playerObject.transform.GetChild(0).gameObject;
		systemManager = GetComponent<SystemManager>();

		if (playerShip.GetComponent<InertialessMovement>()) {
			inertialessMovement = playerShip.GetComponent<InertialessMovement>();
		}

	}

	// Update is called once per frame
	void Update () {
		if (inertialessMovement != null) {

		inertialessMovement.horizontalInput = -Input.GetAxis("Horizontal");

			if (Input.GetAxis("Vertical") > 0) {
				inertialessMovement.thrustersOn = true;
				inertialessMovement.brakesOn = false;
			} else if (Input.GetAxis("Vertical") < 0) {
				inertialessMovement.thrustersOn = false;
				inertialessMovement.brakesOn = true;
			} else {
				inertialessMovement.thrustersOn = inertialessMovement.brakesOn = false;
			}


		}

		if (Input.GetKeyDown("1")) {
			systemManager.LoadSystemData("Sol");
		} else if (Input.GetKeyDown("2")) {
			systemManager.LoadSystemData("Centauri");
		}

	}
}
