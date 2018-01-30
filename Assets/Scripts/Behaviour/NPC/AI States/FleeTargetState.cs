using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeTargetState : AIState {


	protected override void LessThanMinimumDistance() {
		ShortDistance();
	}

	protected override void MinimumDistance() {
		ShortDistance();
	}

	protected override void ShortDistance() {
		FleeTarget();
	}

	protected override void MediumDistance() {
		//Do nothing;
	}


	protected override void LongDistance() {
		SlowToEngage();
	}


}
