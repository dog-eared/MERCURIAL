using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InertialessMovement : MonoBehaviour {

	public float rotateSpeed = 1f;
	public float acceleration = 0.09f;
	public float deacceleration = 0.14f;
	public float maximumSpeed = 20f;

	[HideInInspector]
	public bool thrustersOn;
	[HideInInspector]
	public bool brakesOn;
	[HideInInspector]
	public float horizontalInput;

	public float currentThrust;


	void Update() {

		currentThrust = Mathf.Clamp(currentThrust, 0, maximumSpeed);


	}


	void FixedUpdate () {
		ShipMovement();

		if (horizontalInput != 0) {
			ShipRotate(horizontalInput);
		}

		if (thrustersOn) {
			ShipThrust(acceleration);
		} else if (brakesOn) {
			ShipBrakes(deacceleration);
		}
	}

	public virtual void ShipMovement() {
		transform.Translate(Vector3.up * Time.deltaTime * currentThrust);
	}

	void ShipRotate(float horizontalInput) {
			transform.Rotate(0, 0, horizontalInput * Time.deltaTime * rotateSpeed, Space.World);
	}

	public virtual void ShipThrust(float acceleration) {
		currentThrust += acceleration;
	}

	void ShipBrakes(float deacceleration) {
		currentThrust -= deacceleration;
	}


	void OnCollisionEnter2D(Collision2D collidedWith) {
		Debug.Log("Collision!  : " + collidedWith);
	}


}
