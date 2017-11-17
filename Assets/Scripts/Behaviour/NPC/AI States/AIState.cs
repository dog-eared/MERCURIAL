using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour {


	/*AI State

	Individual state for AI from which all other states are derived. Handles all
	the logic of how to behave, accomplish goals and provide feedback to the parent
	AI.

	For now, I'm	not making this an abstract class until I'm confident in my
	implementation, given that I'll likely be using it to test all of the utility
	methods I have to create.


	All tasks should be split into and called by either Update or Periodic Update.
	Update should handle things that are analogous to player input -- turning,
	accelerating, shooting, etc. Periodic Update handles tracking the location of
	our target, setting new targets, etc.


	*/

	BaseAI _parentBaseAI;

	ShipChassis _chassis;
	Rigidbody2D _rb2d;

	//public float periodicUpdateFrequency = 1f; //How often the periodic update function is called

	public GameObject targetedShip; //Ship we're working in relation to
	public Vector3 targetLocation;  //Either position of ship, or just an abstract pos.

	//public bool killTarget;

	public bool facingTarget;
	public bool facingAway;

	bool slowingDown;

	public float rotationTolerance = 30f;

	float rotationSpeed;

	public float distanceToTarget;
	public float velocityMagnitude;

	public float eulerAngleToTarget;
	public float brakeAngleToTarget;

	[Space(10)]
	[Header("Distances:")]
	public float longDistance = 50f;
	public float mediumDistance = 20f;
	public float shortDistance = 10f;
	public float minDistance = 4f;

	private Quaternion rotationToTarget;
	private Vector3 vectorToTarget;


	void Awake() {
		_parentBaseAI = GetComponent<BaseAI>();
		_chassis = GetComponent<ShipChassis>();
 		rotationSpeed = _chassis.rotateSpeed / 150;
		_rb2d = GetComponent<Rigidbody2D>();
		//InvokeRepeating("PeriodicUpdate", 0, periodicUpdateFrequency);
	}


	void Update () {
		if (targetedShip != null) {
			SetTargetShipVector(targetedShip);
		} else {
			vectorToTarget = Vector3.zero;
		}
		vectorToTarget = (targetLocation - transform.position).normalized;
		eulerAngleToTarget = GetAngleToTarget(targetLocation);
		distanceToTarget = (targetLocation - transform.position).magnitude;

		if (!facingTarget) {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg
														- 90 + Random.Range(-rotationTolerance, rotationTolerance)), Time.deltaTime * rotationSpeed);
														//Slerps from our rotation to a quat.euler
			_chassis.thrustersOn = false;
		} else if (distanceToTarget > minDistance) {
			_chassis.thrustersOn = true;
			_chassis.brakesOn = false;
		} else {
			_chassis.brakesOn = true;
		}

		if (facingTarget && distanceToTarget < mediumDistance && targetedShip != null) {
			_chassis.shipWeapons[0].FireButtonPressed();
		}


		SetFacingStatus();

	}


	void SetFacingStatus() {
		//Set whether or not we're facing our target or facing away from the target.
		if (transform.eulerAngles.z <= eulerAngleToTarget + AdjustedTolerance() && transform.eulerAngles.z >= eulerAngleToTarget - AdjustedTolerance()) {
			facingTarget = true;
		} else {
			facingTarget = false;
		}

		if (transform.eulerAngles.z <= brakeAngleToTarget + AdjustedTolerance() && transform.eulerAngles.z >= brakeAngleToTarget - AdjustedTolerance()) {
			facingAway = true;
		} else {
			facingAway = false;
		}
	}


	float AdjustedTolerance() {
		if (distanceToTarget > longDistance) {
			return rotationTolerance / 2;
		} else if (distanceToTarget > mediumDistance) {
			return rotationTolerance;
		} else if (distanceToTarget > shortDistance) {
			return rotationTolerance / 2;
		}
		return rotationTolerance / 3;
	}


	void SetTargetShipVector(GameObject ship) {
		targetLocation = ship.transform.position;
	}


	float GetDistanceToTarget(Vector3 target) {
		Vector3 distance = transform.position - target;
		return distance.magnitude;
	}

	float GetAngleToTarget(Vector3 target) {
		Vector3 vectorDifference = transform.position - target;
		float tanOpposite = vectorDifference.y;
		float tanAdjacent = vectorDifference.x;

		float targetAngle = Mathf.Atan2(tanOpposite, tanAdjacent) * Mathf.Rad2Deg;
		return Mathf.Repeat(targetAngle + 90, 360);
	}


}
