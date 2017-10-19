using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackdropScroller : MonoBehaviour {

	public GameObject backdrop;
	public GameObject playerShip;
	public Camera mainCam;

	MeshRenderer backdropRenderer;

	public float scrollSpeedReduction = 180;

	void Awake() {
		backdropRenderer = backdrop.GetComponent<MeshRenderer>();
		mainCam = Camera.main;
	}

	void FixedUpdate() {
		if (playerShip != null) {
			float renderX = mainCam.transform.position.x / scrollSpeedReduction;
			float renderY = mainCam.transform.position.y / scrollSpeedReduction;
			backdropRenderer.material.mainTextureOffset = new Vector2(renderX, renderY);
			transform.position = new Vector2(playerShip.transform.position.x, playerShip.transform.position.y);
		}


	}

	public void DisplayBackdrop(Material newBackdrop) {
		backdropRenderer.material = newBackdrop;
	}

}
