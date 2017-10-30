using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : ShipChassis {

	/* PHYSICS MOVEMENT

	Most common implementation of movement for ships. Ships accelerate
	while thrusters are on, and maintain inertia until stopped.

	*/


	Rigidbody2D rigidbody2D;

	public override void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D>();

		base.Awake();

	}

	public override void ShipMovement() {
		rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maximumSpeed);
		}

	public override void ShipThrust(float acceleration) {
		currentThrust += acceleration;
		rigidbody2D.AddForce(transform.up * currentThrust);
	}

}
