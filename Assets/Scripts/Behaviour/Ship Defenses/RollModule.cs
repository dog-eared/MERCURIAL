using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollModule : ShipDefense {

	/* ROLL MODULE

	Allows a ship to roll, moving them directly left or right from their current
	position.


	*/

	public AudioClip warpSound;
	public AudioManager audioManager;
	public GameObject gameManager;

	public float rollTime = 1f;
	public float rollSpeed = 0.01f;

	bool rollingLeft;
	bool rollingRight;

	float rollFinishTime = 0f;
	ShipStateBehaviour shipState;


	void Awake() {
		defenseName = "Roll Module : " + direction;
		shipState = GetComponent<ShipStateBehaviour>();
		audioManager = gameManager.GetComponent<AudioManager>();
	}


	bool CheckBothRolls() {
		return (rollingLeft || rollingRight);
	}


	void StopRolling() {
		rollingLeft = rollingRight = false;
		shipState.RemoveState("Rolling");
		rollFinishTime = Time.time;
	}


	public override void DefenseRotateLeft() {
		if (CheckBothRolls() == false && buttonBeingHeld) {
			Debug.Log("Starting to add roll");
			shipState.AddState("Rolling");
			rollingLeft = true;
			Invoke("StopRolling", rollTime);
			audioManager.PlaySingleFX(warpSound, 0);
		}
	}

	public override void DefenseRotateRight() {
		if (CheckBothRolls() == false && buttonBeingHeld) {
			Debug.Log("Starting to add roll");
			shipState.AddState("Rolling", 2f);
			rollingRight = true;
			Invoke("StopRolling", rollTime);
			audioManager.PlaySingleFX(warpSound, 0);
		}
	}

	void FixedUpdate() {

			if (rollingLeft) {
				transform.Translate(Vector3.right * -rollSpeed);
			} else if (rollingRight) {
				transform.Translate(Vector3.right * rollSpeed);
			}
	}

}
