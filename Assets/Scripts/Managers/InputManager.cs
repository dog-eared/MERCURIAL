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

	public GUIManager _guiManager;


	void Awake () {
		playerObject = GameObject.FindWithTag("PlayerObject");
		playerShip = playerObject.transform.GetChild(0).gameObject;
		systemManager = GetComponent<SystemManager>();
		shipState = playerShip.GetComponent<ShipStateBehaviour>();

		gameCameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();


		if (playerShip.GetComponent<ShipChassis>()) {
			shipChassis = playerShip.GetComponent<ShipChassis>();
			playerShip.GetComponent<ShipData>()._guiManager =
			GameObject.FindGameObjectWithTag("GameController").GetComponent<GUIManager>();
			//Debug.Log("Got player chassis");
		} else {
			Debug.Log("No chassis found.");
		}

	}

	// Update is called once per frame
	void Update () {

		/*Movement while Defenses are active*/
		if (Input.GetAxis("Horizontal") <= -defenseActivationThreshold) {
			shipChassis.shipDefenses[0].DefenseRotateLeft();
			shipChassis.shipDefenses[1].DefenseRotateLeft();
		} else if (Input.GetAxis("Horizontal") >= defenseActivationThreshold) {
			shipChassis.shipDefenses[0].DefenseRotateRight();
			shipChassis.shipDefenses[1].DefenseRotateRight();
		}

		if (shipState.GetTopState().canMove == true) {
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
		} else {
			shipChassis.thrustersOn = false;
			shipChassis.brakesOn = false;
		}

		/*Activating/Deactivating Defenses L R*/
		if (shipState.GetTopState().canDefend == true) {
			if (Input.GetButtonDown("DefenseL")) {
				shipChassis.shipDefenses[0].DefenseButtonPressed();
			}

			if (Input.GetButtonDown("DefenseR")) {
				shipChassis.shipDefenses[1].DefenseButtonPressed();
			}

		}

		/* Releasing our buttons is outside the canDefend so they don't get stuck
		down  */

		if (Input.GetButtonUp("DefenseL")) {
			shipChassis.shipDefenses[0].DefenseButtonReleased();
		}

		if (Input.GetButtonUp("DefenseR")) {
			shipChassis.shipDefenses[1].DefenseButtonReleased();
		}

		/*Rotation*/
		//Kept separate from defenses below in order to ensure rotates cut off when
		//axis input is zero.
		if (shipState.GetTopState().canRotate == true) {
			shipChassis.horizontalInput = -Input.GetAxis("Horizontal");
		} else {
			shipChassis.horizontalInput = 0;
		}

		if (shipState.GetTopState().canShoot == true) {
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
		}

		/* GUI controls */

		if (Input.GetButtonDown("MoveLogs") && _guiManager != null) {
			_guiManager.MoveLogs();
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
			systemManager.LoadSystemData("Alpha Centauri");
		}
	}

}
