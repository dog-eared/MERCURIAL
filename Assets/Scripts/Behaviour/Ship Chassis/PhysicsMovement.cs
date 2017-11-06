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
	}

	public override void ShipBrakes(float deacceleration) {
		//
	}

}
