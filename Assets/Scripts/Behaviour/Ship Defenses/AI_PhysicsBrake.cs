using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PhysicsBrake : ShipDefense {

	//Only to be used by AI Ships
	//Allows for slowdown when necessary


	Rigidbody2D _rb2d;

	void Awake() {
		_rb2d = GetComponent<Rigidbody2D>();
	}

	public virtual void DefenseButtonPressed() {
		_rb2d.drag += 3f;
	}

	public virtual void DefenseButtonReleased() {
		_rb2d.drag = 0f;
	}

}
