using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {

	public string shipModel; //Style of ship
	public string shipName; //Name of ship
	public GameObject explosionAnimation;

	[Space(10)]

	[Header("Combat Stats:")]

	public float shipHullCurrent;
	public float shipHullMax;

	public float shipShieldCurrent;
	public float shipShieldMax;



	void Awake() {
		this.name = shipName;
	}

	void Update() {
		if (Input.GetKeyDown("x")) {
			Death();
		}
	}

	void Death() {
		Instantiate(explosionAnimation, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
		Destroy(gameObject);
	}

}
