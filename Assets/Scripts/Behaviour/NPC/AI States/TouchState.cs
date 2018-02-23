using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchState : AIState {

	void Awake() {
		base.Awake();
		if (SystemInfo.deviceType != DeviceType.Handheld) {
			this.enabled = false;
		}
	}


	protected override void LessThanMinimumDistance() {
		//Do nothing
	}


	protected override void MinimumDistance() {

			//Do nothing
	}


	protected override void ShortDistance() {

			//Do nothing
	}


	protected override void MediumDistance() {

			//Do nothing

	}

	protected override void LongDistance() {

			//Do nothing
	}

	public void TouchPoint(Vector3 point) {
		Debug.Log("Touch found");
		//Used for touch controls if enabled. Used when touching empty space;
		targetLocation = point;
		RotateToTarget();
	}
}
