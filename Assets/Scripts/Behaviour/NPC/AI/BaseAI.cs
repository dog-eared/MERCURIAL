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

	AIState currentState;

	public Rigidbody2D rb2d;
	public AIState normalAI; //Normal state active when nothing is going on.
	public AIState combatAI; //State used when threatened in combat.
	public AIState specialAI; //Special circumstances AI.

	public ShipData _shipData;

	protected virtual void Awake() {
		rb2d.drag = 4f;

		AllStatesOff();
		SetAIState(normalAI);

		_shipData = GetComponent<ShipData>();

		_shipData._ai = this;
		Debug.Log("Assigned ship data");

	}


	void SetAIState(AIState newState) {
		AllStatesOff();
		currentState = newState;
		currentState.enabled = true;
	}


	void AllStatesOff() {
		Debug.Log("All states off start");
		normalAI.enabled = false;
		combatAI.enabled = false;
		if (specialAI != null) {
			specialAI.enabled = false;
		}
	}


	public void IWasHit(GameObject offender) {
		if (currentState != combatAI) {

			AggroTarget(offender);
		}
	}


	public void AllyWasHit(GameObject offender) {
		IWasHit(offender);
	}

	public void IShouldGoTo(Vector2 location) {
		normalAI.targetLocation = location;
	}

	public void AggroTarget(GameObject target) {
		combatAI.targetAliveFlag = true;
		combatAI.targetedShip = target;
		SetAIState(combatAI);
	}


}
