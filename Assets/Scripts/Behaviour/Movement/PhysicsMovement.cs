using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : InertialessMovement {

	Rigidbody2D rigidbody2D;

	void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D>();

	}
	
	public override void ShipMovement() {
		rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maximumSpeed);
		}

	public override void ShipThrust(float acceleration) {
		currentThrust += acceleration;
		rigidbody2D.AddForce(transform.up * currentThrust);
	}

}
