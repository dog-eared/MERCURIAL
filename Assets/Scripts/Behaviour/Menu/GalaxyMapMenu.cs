using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMapMenu : MonoBehaviour {

	/* GALAXY MAP mechanicsBonus

	Menu controls for GALAXY MAP!

	Each node is actually set up as a button in the inspector, which calls the
	SetTargetSystem(string) function with the string set by the button itself.

	This class is meant to handle: zooming in/out, centering/resetting.


	TODO:
		Clamp position so we can't drag into infinity


	*/



	public string targetSystem;
	Vector2 targetPosition;

	public GameStateManager _gameStateManager;
	public List<GameObject> _mapNodes;
	public SystemManager _systemManager;

	public GameObject galaxyMap;
	public GameObject playerMapIcon;

	RectTransform rectTransform;

	public float scaleSpeed = 0.01f;
	public float zoomMax = 2f;
	public float zoomMin = 0.7f;

	void Awake() {
		if (galaxyMap) {
			rectTransform = galaxyMap.GetComponent<RectTransform>();
		}
	}


	public void ZoomInMap(float multiplier) {
		//Multiplier is so that the buttons can do scaleSpeed x 4, while InputManager
		//can smoothly handle zooming in and out
		galaxyMap.transform.localScale += new Vector3(scaleSpeed * multiplier, scaleSpeed * multiplier, 0);

		if (galaxyMap.transform.localScale.x > zoomMax) {
			galaxyMap.transform.localScale = new Vector3(zoomMax, zoomMax, 1);
		}
	}

	public void ZoomOutMap(float multiplier) {
		//as above
		galaxyMap.transform.localScale -= new Vector3(scaleSpeed * multiplier, scaleSpeed * multiplier, 0);

		if (galaxyMap.transform.localScale.x < zoomMin) {
			galaxyMap.transform.localScale = new Vector3(zoomMin, zoomMin, 1);
		}

	}

	public void NormalZoom() {
		galaxyMap.transform.localScale = new Vector3(1, 1, 1);
		rectTransform.anchoredPosition = new Vector3(0, 0, 0);
	}

	public void SetTargetSystem(string target) {
		targetPosition = new Vector2(playerMapIcon.transform.position.x - Input.mousePosition.x, playerMapIcon.transform.position.y - Input.mousePosition.y);
		float rotateToTarget = Mathf.Atan2(targetPosition.y, targetPosition.x) * Mathf.Rad2Deg + 90;
		playerMapIcon.transform.eulerAngles = new Vector3(0, 0, rotateToTarget);

		targetSystem = target;
	}


	public void AcceptButton() {
		if (_systemManager.systemName != targetSystem) {
			_gameStateManager.targetSystem = targetSystem;
		} else {
			Debug.Log("Currently in this system.");
		}
	}

	public void CancelButton() {

	}

	void Update() {
		if (Input.GetKey("+") || Input.GetKey("=")) {
			ZoomInMap(1);
		} else if (Input.GetKey("-")) {
			ZoomOutMap(1);
		}
	}



}
