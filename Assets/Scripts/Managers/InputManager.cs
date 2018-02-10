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
	public SystemManager _systemManager;
	public MinimapManager _minimapManager;
	public GameStateManager _gameStateManager;

	ShipChassis shipChassis;
	CameraBehaviour _cameraBehaviour;
	public ShipStateBehaviour shipState;

	public GUIManager _guiManager;
	public GUIBehaviour _guiBehaviour;

	public float validJumpDistance = 50f; //How far from system center to be able to do a hyperspace jump


	void Awake () {
		playerObject = GameObject.FindWithTag("PlayerObject");
		playerShip = playerObject.transform.GetChild(0).gameObject;
		shipState = playerShip.GetComponent<ShipStateBehaviour>();

		_cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();


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

		if ( _gameStateManager.currentMode != GameMode.Menu && _gameStateManager.currentMode != GameMode.Hyperspace ) {

			if (Input.GetButtonDown("Jump")) {
				//Cancel hyperspace jump.
			}

			if (_gameStateManager.currentMode != GameMode.Menu && _gameStateManager.currentMode != GameMode.Hyperspace) {

					/* Hyperspace Jump */
					if (Input.GetButtonDown("Jump")
					&& _gameStateManager.currentMode != GameMode.Hyperspace
					&& (Mathf.Abs(playerShip.transform.position.x) + Mathf.Abs(playerShip.transform.position.y) > validJumpDistance))
					{
						if (_gameStateManager.targetSystem != null) {
							_gameStateManager.currentMode = GameMode.Hyperspace;
							//shipState.AddState("Hyperspace Jump", 3f);
							_gameStateManager.Invoke("HyperspaceJump", 1.8f);
							_cameraBehaviour.StartCoroutine("BackdropFadeOut");
						} else {
							_guiBehaviour.ReceiveMessage("Invalid hyperspace target.", false);
						}
					} else if (Input.GetButtonDown("Jump")) {
						_guiBehaviour.ReceiveMessage("Too close to system center.", false);
					}
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
				if (shipState.GetTopState().canDefend == true) {
					if (Input.GetButtonDown("DefenseL")) {
						shipChassis.FireDefense(0);
					}

					if (Input.GetButtonDown("DefenseR")) {
						shipChassis.FireDefense(1);
					}

				}

				/* Releasing our buttons is outside the canDefend so they don't get stuck
				down  */

				if (Input.GetButtonUp("DefenseL")) {
					shipChassis.ReleaseDefense(0);
				}

				if (Input.GetButtonUp("DefenseR")) {
					shipChassis.ReleaseDefense(1);
				}

				/*Rotation*/
				//Kept separate from defenses below in order to ensure rotates cut off when
				//axis input is zero.
				shipChassis.horizontalInput = -Input.GetAxis("Horizontal");


				/* Firing Weapons */
				if (Input.GetButtonDown("Fire1")) {
					shipChassis.FireWeapon(0);
				}

				if (Input.GetButtonUp("Fire1")) {
					shipChassis.ReleaseWeapon(0);
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

			/* GUI controls */


				if (Input.GetButtonDown("MoveLogs") && _guiManager != null) {
					_guiManager.MoveLogs();
				}

				/* Camera Control */
				if (Input.GetKey("+") || (Input.GetKey("=")) ) {
					_cameraBehaviour.ZoomIn();
				}

				if (Input.GetKey("-")) {
					_cameraBehaviour.ZoomOut();
				}
			}

		}

	}
