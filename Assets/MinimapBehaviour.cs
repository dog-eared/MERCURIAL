using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapBehaviour : MonoBehaviour {

	/* MINIMAP BEHAVIOUR

	Update behaviour for minimap to stay in position to playerShip.

	*/

	public GameObject playerShip;
	public GameObject minimapSurface;

	void Awake() {
		if (playerShip == null) {
			playerShip = GameObject.FindGameObjectWithTag("PlayerObject").transform.GetChild(0).gameObject;
		}
	}

	void FixedUpdate() {
		minimapSurface.transform.localPosition = -playerShip.transform.position;
	}


}
