using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour {

	/*	BASE AI CLASS

	Likely the most common AI class used in game. Simple state manager which activates
	and deactivates the appropriate state when certain criteria are met. The heavy
	lifting of the AI behaviour is handled in the state class that is made active,
	but determining which state is appropriate and listening for events is handled
	here.

	NOTE: In earlier drafts of this AI I had each AI held in an array -- I'd rather
	just add more potential states to derived classes so as to be explicit in the
	inspector with what's going on.

	*/

	AIState currentAIState;

	public Rigidbody2D rb2d;
	public AIState normalAI; //Normal state active when nothing is going on.
	public AIState combatAI; //State used when threatened in combat.
	public AIState specialAI; //Special circumstances AI.

	void Awake() {
		rb2d.drag = 4f;
	}


	void SetAIState(AIState newState) {
		currentAIState = newState;
		AllStatesOff();
		newState.gameObject.SetActive(true);
	}


	void AllStatesOff() {
		normalAI.gameObject.SetActive(false);
		combatAI.gameObject.SetActive(false);
		if (specialAI != null) {
			specialAI.gameObject.SetActive(false);
		}
	}






}
