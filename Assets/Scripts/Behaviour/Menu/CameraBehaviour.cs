using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	Camera mainCam;

	public float zoomIncrement = 0.05f;
	public float zoomOut = 10f;
	public float zoomIn = 1f;


	void Awake() {
		mainCam = Camera.main;
	}

	public void ZoomIn() {
		if (mainCam.orthographicSize > zoomIn) {
			mainCam.orthographicSize -= zoomIncrement;
		}
	}

	public void ZoomOut() {
		if (mainCam.orthographicSize < zoomOut) {
			mainCam.orthographicSize += zoomIncrement;
		}
	}

}
