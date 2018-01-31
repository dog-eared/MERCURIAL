using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

	/* CAMERA BEHAVIOUR

	Handles controls for camera.

	Also handles visual effects that affect the entire camera.

	*/

	Camera mainCam;

	[Header("Config:")]
	public float zoomIncrement = 0.05f;
	public float zoomOut = 10f;
	public float zoomIn = 1f;

	public GameObject playerShip;
	public GameObject backdrop;
	[Header("Effects:")]
	public GameObject fadeWhite;
	public GameObject hyperspaceEffect;

	ForesightModule foresightModule;
	Vector2 foresightLocation;

	MeshRenderer backdropRenderer;
	SpriteRenderer fadeWhiteRenderer;

	public Light _backdropLight;
	public float fadeLerp;

	public float scrollSpeedReduction = 24;

	public IEnumerator fadeCoroutine;

	void Awake() {
		backdropRenderer = backdrop.GetComponent<MeshRenderer>();
		fadeWhiteRenderer = fadeWhite.GetComponent<SpriteRenderer>();
		mainCam = Camera.main;


		fadeWhite.transform.localScale = new Vector3(Screen.width * 4, Screen.height * 4, 1);

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
		hyperspaceEffect.SetActive(true);

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
		hyperspaceEffect.SetActive(false);
	}


	public IEnumerator FadeWhiteToAlphaValue(float targetAlpha) {
		float fadeWhiteLerp = 0;
		float direction;
		float startValue = fadeWhiteRenderer.color.a;
		Color newValue = fadeWhiteRenderer.color;

		if (startValue > targetAlpha) {
			direction = -1;
		} else {
			direction = 1;
		}

		while (fadeWhiteRenderer.color.a != targetAlpha) {
			fadeWhiteLerp += direction * Time.deltaTime;
			newValue.a = Mathf.Lerp(startValue, targetAlpha, fadeWhiteLerp);
			fadeWhiteRenderer.color = newValue;
			yield return null;
		}
	}


	public void DisplayBackdrop(Material newBackdrop) {
		backdropRenderer.material = newBackdrop;
	}


}
