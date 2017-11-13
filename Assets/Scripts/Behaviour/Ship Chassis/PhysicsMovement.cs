using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : ShipChassis {

	/* PHYSICS MOVEMENT

	Most common implementation of movement for ships. Ships accelerate
	while thrusters are on, and maintain inertia until stopped.

	*/

	Rigidbody2D rb2d;
	public Vector3 reverseVelocity;

	bool inputSinceBrakes = false;

	float targetAngle;
	float rotationTolerance = 5f;

	public override void Awake() {
		rb2d = GetComponent<Rigidbody2D>();

		base.Awake();

	}

	public override void ShipMovement() {
		rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maximumSpeed);

		}

	public override void ShipThrust(float acceleration) {
		currentThrust += acceleration;
		rb2d.AddForce(transform.up * currentThrust);
		ShipInput();
	}

	public override void ShipRotate(float horizontalInput) {
		base.ShipRotate(horizontalInput);
		ShipInput();
	}

	void BrakesRotate(float falseHorizontalInput) {
		base.ShipRotate(falseHorizontalInput);
	}


	void ShipInput() {
		if (!inputSinceBrakes) {
			inputSinceBrakes = true;
		}
	}


	public override void ShipBrakes(float deacceleration) {
		Debug.Log(horizontalInput);
		if (inputSinceBrakes) {
			float longVelocity = Mathf.Abs(rb2d.velocity.x) > Mathf.Abs(rb2d.velocity.y) ? rb2d.velocity.x : rb2d.velocity.y;
			float shortVelocity = Mathf.Abs(rb2d.velocity.x) < Mathf.Abs(rb2d.velocity.y) ? rb2d.velocity.x : rb2d.velocity.y;
			targetAngle = Mathf.Atan2(shortVelocity, longVelocity) * Mathf.Rad2Deg;
			targetAngle = Mathf.Repeat(transform.eulerAngles.z - 180, 360);
			Debug.Log(targetAngle + " <----TARGET ANGLE");
			inputSinceBrakes = false;
		} else {
			if (transform.eulerAngles.z > targetAngle + rotationTolerance) {
				BrakesRotate(-1f);
			} else if (transform.eulerAngles.z < targetAngle - rotationTolerance) {
				BrakesRotate(1f);
			}
		}
	}

}
