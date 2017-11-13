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

	bool movementSinceBrakes = false; //NOTE: ShipData may need to set this to "true"
																		//when the player is hit if we want them to be able
																		//to cleanly reverse out of weapon impacts that have
																		//substantial physics.

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
		//Function for rotating the ship without setting movementSinceBrakes flag
		//This lets us control when the targetAngle is set by hitting brakes.
		base.ShipRotate(falseHorizontalInput);
	}


	void ShipInput() {
		if (!movementSinceBrakes) {
			movementSinceBrakes = true;
		}
	}


	public override void ShipBrakes(float deacceleration) {
		if (movementSinceBrakes) {

			float tanOpposite = rb2d.velocity.y;
			float tanAdjacent = rb2d.velocity.x;

			targetAngle = Mathf.Atan2(tanOpposite, tanAdjacent) * Mathf.Rad2Deg;

			targetAngle = Mathf.Repeat(targetAngle + 90, 360);

			movementSinceBrakes = false;

		} else {

			if (transform.eulerAngles.z > targetAngle + rotationTolerance) {
				BrakesRotate(-1f);
			} else if (transform.eulerAngles.z < targetAngle - rotationTolerance) {
				BrakesRotate(1f);

			}
		}
	}

}
