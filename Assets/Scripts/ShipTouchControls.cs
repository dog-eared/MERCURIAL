using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTouchControls : MonoBehaviour {

	public TouchState _touchAI;

	public bool touchEnabled = false;
	bool movementOn = false;

	void Awake () {
		Input.simulateMouseWithTouches = true;

		if (SystemInfo.deviceType == DeviceType.Handheld) {
			touchEnabled = true;
		}
		
	}


	void Update () {

		if (Input.GetMouseButton(0) && touchEnabled) {
			movementOn = true;
		}

		if (!Input.GetMouseButton(0) && touchEnabled) {
			movementOn = false;
		}

	}

	void FixedUpdate() {
		if (_touchAI != null && movementOn) {
			_touchAI.TouchPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		}
	}


}
