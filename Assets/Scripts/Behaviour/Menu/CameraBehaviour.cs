using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	Camera mainCam;

	public float zoomIncrement = 0.05f;
	public float zoomOut = 10f;
	public float zoomIn = 1f;

	public GameObject backdrop;
	public GameObject playerShip;

	ForesightModule foresightModule;
	Vector2 foresightLocation;

	MeshRenderer backdropRenderer;

	public Light _backdropLight;
	public float fadeLerp;

	public float scrollSpeedReduction = 24;

	void Awake() {
		backdropRenderer = backdrop.GetComponent<MeshRenderer>();
		mainCam = Camera.main;

		if (playerShip.GetComponent<ForesightModule>()) {
			foresightModule = playerShip.GetComponent<ForesightModule>();
		} else {
			foresightLocation = new Vector2(0, 0);
		}
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


	void FixedUpdate() {
		if (playerShip != null) {

			if (foresightModule != null) {
				foresightLocation = foresightModule.foresightLocation;
			}

			float renderX = mainCam.transform.position.x / scrollSpeedReduction;
			float renderY = mainCam.transform.position.y / scrollSpeedReduction;

			backdropRenderer.material.mainTextureOffset = new Vector2(renderX, renderY);

			transform.position = new Vector2(playerShip.transform.position.x, playerShip.transform.position.y) + foresightLocation;

		}
	}


	public IEnumerator BackdropFadeOut() {
		while (fadeLerp < 1) {
			fadeLerp += 1f * Time.deltaTime;

			fadeLerp = Mathf.Clamp(fadeLerp, 0, 1);
			_backdropLight.intensity = Mathf.Lerp(2, 0, fadeLerp);

			yield return null;
		}
	}


	public IEnumerator BackdropFadeIn() {
		while (fadeLerp > 0) {
			fadeLerp -= (1f * Time.deltaTime);

			//fadeLerp = Mathf.Clamp(fadeLerp, 0, 1);
			_backdropLight.intensity = Mathf.Lerp(2, 0, fadeLerp);

			yield return null;
		}
	}




	public void DisplayBackdrop(Material newBackdrop) {
		backdropRenderer.material = newBackdrop;
	}


}
