﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipChassis : MonoBehaviour {

	/* SHIP CHASSIS

	Generic chassis class that all other chassis inherit from.

	Has inertialess movement, 3 weapon slots and 3 defensive slots.



	*/

	protected Quaternion reverseAngle;

	public float rotateSpeed = 300f;
	public float acceleration = 0.3f;
	public float deacceleration = 0.44f;
	public float maximumSpeed = 13f;

	[HideInInspector]
	public bool thrustersOn;
	[HideInInspector]
	public bool brakesOn;
	[HideInInspector]
	public float horizontalInput;
	public float currentThrust;

	public List<ShipWeapon> shipWeapons = new List<ShipWeapon>();
	public List<ShipDefense> shipDefenses = new List<ShipDefense>();


	public virtual void Awake() {
		var weaponsToAdd = GetComponents<ShipWeapon>();
		var defensesToAdd = GetComponents<ShipDefense>();

		for (var x = 0; x < weaponsToAdd.Length; x++) {
			shipWeapons.Add(weaponsToAdd[x]);
		}

		while (shipWeapons.Count < 3) {
			var x = gameObject.AddComponent(typeof(EmptyWeaponSlot)) as EmptyWeaponSlot;
			shipWeapons.Add(x);
		}

		for (var x = 0; x < defensesToAdd.Length; x++) {
			shipDefenses.Add(defensesToAdd[x]);
		}

		while (shipDefenses.Count < 2) {
			var x = gameObject.AddComponent(typeof(EmptyDefenseSlot)) as EmptyDefenseSlot;
			shipDefenses.Add(x);
		}

		shipDefenses[1].direction = "right";

	}

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

	public virtual void ShipThrust(float acceleration) {
			currentThrust += acceleration;
	}

	public virtual void ShipRotate(float horizontalInput) {
			transform.Rotate(0, 0, horizontalInput * Time.deltaTime * rotateSpeed, Space.World);
	}


	public virtual void ShipBrakes(float deacceleration) {
		currentThrust -= deacceleration;
	}


	void OnCollisionEnter2D(Collision2D collidedWith) {
		Debug.Log("Collision!  : " + collidedWith);
	}


}
