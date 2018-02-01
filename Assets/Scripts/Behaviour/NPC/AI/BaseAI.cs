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

	[Header("Attacked Config:")]
	public float attackedMemory = 40f; //How long until they forget they were attacked.
	public bool attackedRecently = false; //Instance of aggression since player encountered them
	//Basically this exists to reduce expensive calls for aggression

	public List<GameObject> targets = new List<GameObject>();

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
		if (!attackedRecently) {
			if (currentState != combatAI) {
				AggroTarget(offender);
				attackedRecently = true;
				Invoke("ToggleOffAttacked", attackedMemory);
			}
		}
	}

	public void IShouldGoTo(Vector2 location) {
		normalAI.targetLocation = location;
	}

	public void AggroTarget(GameObject target) {
		combatAI.targetAliveFlag = true;
		combatAI.targetedShip = target;
		SetAIState(combatAI);
	}

	void ToggleOffAttacked() {
		attackedRecently = false;
	}

	protected void CheckTargetDead() {
    if (targets[0] == null) {
      targets.Remove(targets[0]);
    } else {
      AggroTarget(targets[0]);
    }
  }


  protected void AddTargetsToList(GameObject[] targetsList) {
    for (var x = 0; x < targetsList.Length; x++) {
      if (targetsList[x] != null && targetsList[x].layer == 13) {
        targets.Add(targetsList[x]);
      }
    }
  }


  protected float CalculateDistanceToPoint(GameObject a, GameObject b) {
    return Mathf.Abs(a.transform.position.y - b.transform.position.y) /
            (a.transform.position.x - b.transform.position.x);
  }


	protected void GetNearestTarget() {
		if (targets.Count != 0) {
			float distance = CalculateDistanceToPoint(targets[0], this.gameObject);
			int swapPlace = 0;
			GameObject temp = targets[0];

			for (int x = 0; x < targets.Count; x++) {
				if (CalculateDistanceToPoint(targets[x], this.gameObject) < distance) {
					swapPlace = x;
				}
			}

			targets[0] = temp;
			targets[0] = targets[swapPlace];
			targets[swapPlace] = temp;

			if (combatAI.enabled) {
				AggroTarget(targets[0]);
			}
		}


  }

	protected virtual void GetTargets() {
		//Dummy method. If they don't fight, they don't need this method!
	}

}
