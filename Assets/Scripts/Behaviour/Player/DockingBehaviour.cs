using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingBehaviour : MonoBehaviour {

	public bool attemptingDocking = false;
	float lastDockAttempt;
	BoxCollider2D boxCollider2D;

	public string planetTarget;

	void Awake() {
		boxCollider2D = GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("Collision!");
		if (collider.tag == "Planet") {
			planetTarget = collider.gameObject.name;
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.tag == "Planet") {
			planetTarget = "";
		}
	}
	// Update is called once per frame
	void Update () {
		if (attemptingDocking) {

		}
	}
}
