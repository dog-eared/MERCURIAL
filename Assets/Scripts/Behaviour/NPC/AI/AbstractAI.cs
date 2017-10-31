using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractAI : MonoBehaviour {

	/*ABSTRACT AI

	Abstract AI class that our other AIs will inherit from. The AI controls which
	state is active by activating the scripts for each state based on when the
	appropriate public methods are called by another object.

	Our CheckStatesForCondition function iterates through states in order (by
	going through our AIStateTrigger enum) until it reaches the first one with the
	appropriate trigger associated, then sets the state to that function.


	Any AI variant should basically have a custom Awake function -- one that
	generates a couple of states and attaches them, then stores them in our states
	array and sets the default state.


	*/

	ShipChassis chassis;

	public float AITriggerTime = 8f;

	public AIState[] states;
	int currentState = 0;


	void Awake() {
		states = GetComponents<AIState>();
		chassis = GetComponent<ShipChassis>();
		SwapToState(0);
	}


	void Update() {
		chassis.thrustersOn = states[currentState].thrustersOn;
		chassis.brakesOn = states[currentState].brakesOn;
		chassis.horizontalInput = states[currentState].rotation;
	}



	public void allyWasHit() {
		Debug.Log(this.name + " : my ally was hit, triggering a state.");
		int targetState = CheckStatesForCondition(AIStateTrigger.allyWasHit);

		SwapToState(targetState);
	}

	public void iWasHit() {
		Debug.Log(this.name + "I was hit, triggering a state.");
		int targetState = CheckStatesForCondition(AIStateTrigger.iWasHit);

		SwapToState(targetState);
	}

	public void specialTrigger() {
		Debug.Log(this.name + ": A special trigger has changed my state.");
		int targetState = CheckStatesForCondition(AIStateTrigger.special);

		SwapToState(targetState);
	}


	int CheckStatesForCondition(AIStateTrigger checkTrigger) {
		for (var x = 0; x < states.Length; x++) {
			if (states[x].trigger == checkTrigger) {
				return x;
			}
		}
		return 0;
	}


	void SwapToState(int targetState) {
		for (var x = 0; x < states.Length; x++) {
			states[x].enabled = false;
		}

		states[targetState].enabled = true;
	}


}



public enum AIStateTrigger {
	normal,
	allyWasHit,
	iWasHit,
	special
}
