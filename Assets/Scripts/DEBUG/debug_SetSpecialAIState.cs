using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_SetSpecialAIState : MonoBehaviour {

	public AbstractAI targetAI;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("u")) {
			targetAI.specialTrigger();
		}

		if (Input.GetKeyDown("y")) {
			targetAI.allyWasHit();
		}

	}
}
