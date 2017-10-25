using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingBehaviour : MonoBehaviour {

	public string planetTarget;

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


}
