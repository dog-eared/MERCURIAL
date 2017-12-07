using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipStateBehaviour))]
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

	ShipData _shipData;
	ShipStateBehaviour _shipState;


	public virtual void Awake() {

		_shipState = GetComponent<ShipStateBehaviour>();

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


	void FixedUpdate () {
		ShipMovement();
		currentThrust = Mathf.Clamp(currentThrust, 0, maximumSpeed);

		if (horizontalInput != 0) {
			if (_shipState.GetTopState().canRotate == true) {
				ShipRotate(horizontalInput);
			} else {
				horizontalInput = 0;
			}
		}

		if (_shipState.GetTopState().canMove == true) {
			if (thrustersOn) {
				ShipThrust(acceleration);
			} else if (brakesOn && _shipState.GetTopState().canRotate == true) {
				ShipBrakes(deacceleration);
			}
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

	public virtual void FireWeapon(int weapon) {
		if (shipWeapons[weapon] != null && _shipState.GetTopState().canShoot == true) {
			shipWeapons[weapon].FireButtonPressed();
		}
	}

	public virtual void ReleaseWeapon(int weapon) {
		if (shipWeapons[weapon] != null) {
			shipWeapons[weapon].FireButtonReleased();
		}
	}

	public virtual void FireDefense(int defense) {
		if (shipDefenses[defense] != null && _shipState.GetTopState().canDefend == true) {
			shipDefenses[defense].DefenseButtonPressed();
		}
	}

	public virtual void ReleaseDefense(int defense) {
		if (shipDefenses[defense] != null) {
			shipDefenses[defense].DefenseButtonReleased();
		}
	}


	void OnCollisionEnter2D(Collision2D collidedWith) {
		Debug.Log("Collision!  : " + collidedWith);
	}


}
