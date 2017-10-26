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

	public float defenseActivationThreshold; //How far the horizontal input needs to be to trigger
																					 //defense addon powers

	SystemManager systemManager;

	ShipChassis shipChassis;
	CameraBehaviour gameCameraBehaviour;
	public ShipStateBehaviour shipState;



	void Awake () {
		playerObject = GameObject.FindWithTag("PlayerObject");
		playerShip = playerObject.transform.GetChild(0).gameObject;
		systemManager = GetComponent<SystemManager>();
		shipState = playerShip.GetComponent<ShipStateBehaviour>();

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

		/*Rotation*/
		//Kept separate from defenses below in order to ensure rotates cut off when
		//axis input is zero.
		if (shipState.GetTopState() == "NormalState") {
			shipChassis.horizontalInput = -Input.GetAxis("Horizontal");
		}

		/*Movement while Defenses are active*/
		if (Input.GetAxis("Horizontal") <= -defenseActivationThreshold) {
			shipChassis.shipDefenses[0].DefenseRotateLeft();
			shipChassis.shipDefenses[1].DefenseRotateLeft();
		} else if (Input.GetAxis("Horizontal") >= defenseActivationThreshold) {
			shipChassis.shipDefenses[0].DefenseRotateRight();
			shipChassis.shipDefenses[1].DefenseRotateRight();
		}


		/*Thrust and Brakes*/
		if (Input.GetAxis("Vertical") > 0) {
			shipChassis.thrustersOn = true;
			shipChassis.brakesOn = false;
		} else if (Input.GetAxis("Vertical") < 0) {
			shipChassis.thrustersOn = false;
			shipChassis.brakesOn = true;
		} else {
			shipChassis.thrustersOn = shipChassis.brakesOn = false;
		}


		/*Activating/Deactivating Defenses L R*/
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


		/* Firing Weapons */
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
		

		/* Camera Control */
		if (Input.GetKey("+") || (Input.GetKey("=")) ) {
			gameCameraBehaviour.ZoomIn();
		}

		if (Input.GetKey("-")) {
			gameCameraBehaviour.ZoomOut();
		}


		/*Debug!!*/
		if (Input.GetKeyDown("1")) {
			systemManager.LoadSystemData("Sol");
		} else if (Input.GetKeyDown("2")) {
			systemManager.LoadSystemData("Centauri");
		}
	}

}
