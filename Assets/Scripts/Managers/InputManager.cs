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

	ShipChassis shipChassis;
	CameraBehaviour gameCameraBehaviour;

	void Awake () {
		playerObject = GameObject.FindWithTag("PlayerObject");
		playerShip = playerObject.transform.GetChild(0).gameObject;
		systemManager = GetComponent<SystemManager>();

		gameCameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();

		if (playerShip.GetComponent<ShipChassis>()) {
			shipChassis = playerShip.GetComponent<ShipChassis>();
			Debug.Log("Got chassis");
		} else {
			Debug.Log("No chassis found.");
		}

	}

	// Update is called once per frame
	void Update () {
		shipChassis.horizontalInput = -Input.GetAxis("Horizontal");

		if (Input.GetAxis("Vertical") > 0) {
			shipChassis.thrustersOn = true;
			shipChassis.brakesOn = false;
		} else if (Input.GetAxis("Vertical") < 0) {
			shipChassis.thrustersOn = false;
			shipChassis.brakesOn = true;
		} else {
			shipChassis.thrustersOn = shipChassis.brakesOn = false;
		}

		if (Input.GetButtonDown("DefenseL")) {
			shipChassis.shipDefenses[0].DefenseButtonPressed();
		}

		if (Input.GetButtonUp("DefenseL")) {
			shipChassis.shipDefenses[0].DefenseButtonReleased();
		}

		if (Input.GetButtonDown("DefenseR")) {
			shipChassis.shipDefenses[1].DefenseButtonPressed();
		}

		if (Input.GetButtonUp("DefenseR")) {
			shipChassis.shipDefenses[1].DefenseButtonReleased();
		}


		if (Input.GetButtonDown("Fire1")) {
			shipChassis.shipWeapons[0].FireButtonPressed();
		}

		if (Input.GetButtonUp("Fire1")) {
			shipChassis.shipWeapons[0].FireButtonReleased();
		}

		if (Input.GetButtonDown("Fire2")) {
			shipChassis.shipWeapons[1].FireButtonPressed();
		}

		if (Input.GetButtonUp("Fire2")) {
			shipChassis.shipWeapons[1].FireButtonReleased();
		}

		if (Input.GetButtonDown("Fire3")) {
			shipChassis.shipWeapons[2].FireButtonPressed();
		}

		if (Input.GetButtonUp("Fire3")) {
			shipChassis.shipWeapons[2].FireButtonReleased();
		}

		if (Input.GetKey("+") || (Input.GetKey("=")) ) {
			gameCameraBehaviour.ZoomIn();
		}

		if (Input.GetKey("-")) {
			gameCameraBehaviour.ZoomOut();
		}


		if (Input.GetKeyDown("1")) {
			systemManager.LoadSystemData("Sol");
		} else if (Input.GetKeyDown("2")) {
			systemManager.LoadSystemData("Centauri");
		}
	}

}
