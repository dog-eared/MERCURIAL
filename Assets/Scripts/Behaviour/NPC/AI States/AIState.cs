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
	public float minimumDistance = 4f;

	[Space(10)]
	[Header("Velocities:")]
	public float engageSpeed = 0.35f;
	[Tooltip("Represents multiplier to slow down ship when engaging in combat")]

	private Quaternion rotationToTarget;
	private Vector3 vectorToTarget;


	void Awake() {
		_parentBaseAI = GetComponent<BaseAI>();
		_chassis = GetComponent<ShipChassis>();
 		rotationSpeed = _chassis.rotateSpeed / 150; //Magic number of 150... pretty good approximation of normal rotation
		_rb2d = GetComponent<Rigidbody2D>();
		//InvokeRepeating("PeriodicUpdate", 0, periodicUpdateFrequency);
	}


	void LessThanMinimumDistance() {
		//For distances less than minimumDistance
		//Normal objective: basically hang out
		MinimumDistance();
	}


	void MinimumDistance() {
		//For distances greater than minimumDistance but less than shortDistance
		RotateToTarget();
	}


	void ShortDistance() {
		//For distances greater than shortDistance but less than mediumDistance
		if (slowingDown) {
			SlowToEngage();
		} else {
			RotateToTarget();
		}
	}


	void MediumDistance() {
		//For distances greater than mediumDistance but less than longDistance
		if (facingTarget) {
			_chassis.thrustersOn = true;
		} else {
			_chassis.thrustersOn = false;
			RotateToTarget();
		}

	}

	void LongDistance() {
		//For distances greater than longDistance
		MediumDistance();
	}


	/*

	UPDATING

	*/


	void LateUpdate () {
		if (targetedShip != null) {
			SetTargetShipVector(targetedShip);
		}

		vectorToTarget = (targetLocation - transform.position).normalized;
		eulerAngleToTarget = GetAngleToTarget(targetLocation);
		distanceToTarget = (targetLocation - transform.position).magnitude;
		velocityMagnitude = _rb2d.velocity.sqrMagnitude;

		if (distanceToTarget < minimumDistance) {
			LessThanMinimumDistance();
		} else if (distanceToTarget < shortDistance) {
			MinimumDistance();
		} else if (distanceToTarget < mediumDistance) {
			ShortDistance();
		} else if (distanceToTarget < longDistance) {
			MediumDistance();
		} else {
			LongDistance();
		}

		if (facingTarget && distanceToTarget < mediumDistance && targetedShip != null) {
			FireAllWeapons();
		}

		SetFacingStatus();


	}

	/*

	METHODS

	*/

	float CalculateTimeToIntercept() {
		Vector3 interceptDistance = transform.position - targetLocation;
		return interceptDistance.sqrMagnitude / velocityMagnitude;


	}


	void ToggleSlowingDown() {
		slowingDown = !slowingDown;
	}


	void SlowToEngage() {
		if (velocityMagnitude > (Mathf.Pow(_chassis.maximumSpeed, 2) * engageSpeed)) {
			if (!facingTarget) {
				RotateToAway();
			} else {
				_chassis.thrustersOn = true;
			}
		} else {
			_chassis.thrustersOn = false;
		}
	}


	void RotateToTarget() {
		if (!facingTarget) {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg
														- 90 + Random.Range(-rotationTolerance, rotationTolerance)), Time.deltaTime * rotationSpeed);
			}
	}

	void RotateToAway() {
		if (!facingAway) {
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg
														+ 90 + Random.Range(-rotationTolerance, rotationTolerance)), Time.deltaTime * rotationSpeed);
		}
	}

	void FireAllWeapons() {
		_chassis.shipWeapons[0].FireButtonPressed();
		_chassis.shipWeapons[1].FireButtonPressed();
		_chassis.shipWeapons[2].FireButtonPressed();
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


public enum Objective {
	Flee,
	Pursue,
	Engage,
	Land,
	Jump
}


class AITask {
	string name;
	GameObject target;
	Vector3 targetLocation;
	Objective objective;

	public Vector3 ReturnLocationVector() {
		if (target != null) {
			return target.transform.position;
		} else {
			return targetLocation;
		}
	}

}
