using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractTask : MonoBehaviour {

	ShipChassis shipChassis;
	Transform shipTransform;

	void Awake() {
		shipChassis = GetComponent<ShipChassis>();
		shipTransform = GetComponent<Transform>();
	}

}
