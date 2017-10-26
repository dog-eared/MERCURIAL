using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateBehaviour : MonoBehaviour {

	int top = 0;
	List<ShipState> shipStateList = new List<ShipState>();


	void Awake() {
		shipStateList.Add(new ShipState());
		GenerateCurrentState();
	}


	public ShipState GetTopState() {
		return shipStateList[top];
	}


	public void AddState(string newState, float duration = -1f) {
		//NEEDS A REFACTOR... listing everything in here is not ideal, but I need
		//to learn more before I can rework it. Come back to this.

		ShipState stateToAdd;

		switch (newState) {
			case "No Move":
				stateToAdd = NoMove();
				break;
			case "No Rotate":
				stateToAdd = NoRotate();
				break;
			case "Disabled":
				stateToAdd = ShipDisabled();
				break;
			case "Rolling":
				stateToAdd = Rolling(duration);
				break;
			default:
				stateToAdd = new ShipState();
				break;
			}

		if (stateToAdd.stateName != "Normal") {
			shipStateList.Add(stateToAdd);
		}

		if (stateToAdd.timeTilRemoval != -1f) {
			Debug.Log("State has a time to remove.");
		}

		GenerateCurrentState();

	}


	public void PopTopState() {
		shipStateList.RemoveAt(shipStateList.Count - 1);
		top = shipStateList.Count;
	}


	public void RemoveState(string state) {
		int stateIndex = -1;

		for (var x = 0; x < shipStateList.Count; x++ ) {
			if (shipStateList[x].stateName == state) {
				stateIndex = x;
			}
		}

		if (stateIndex > -1) {
			shipStateList.RemoveAt(stateIndex);
		}

		GenerateCurrentState();

	}


	ShipState GenerateCurrentState() {
		if (shipStateList[shipStateList.Count - 1].stateName == "Current State") {
			PopTopState();
		}

		var currentState = new ShipState();
		currentState.stateName = "Current State";

		foreach(ShipState state in shipStateList) {
			if (state.canMove == false) {
				currentState.canMove = false;
			}
			if (state.canRotate == false) {
				currentState.canRotate = false;
			}
			if (state.canDefend == false) {
				currentState.canDefend = false;
			}
			if (state.canShoot == false) {
				currentState.canShoot = false;
			}
		}

		top = shipStateList.Count - 1;
		return currentState;
	}

	/* FUNCTIONS FOR GENERATING STATES */

	ShipState NoMove() {
		var noMoveState = new ShipState();
		noMoveState.stateName = "No Move";
		noMoveState.canMove = false;
		return noMoveState;
	}


	ShipState NoRotate() {
		var noRotateState = new ShipState();
		noRotateState.stateName = "No Rotate";
		noRotateState.canRotate = false;
		return noRotateState;
	}


	ShipState ShipDisabled() {
		var disabled = new ShipState();
		disabled.stateName = "Disabled";
		disabled.canMove = false;
		disabled.canRotate = false;
		disabled.canDefend = false;
		disabled.canShoot = false;
		return disabled;
	}

	ShipState Rolling(float duration) {
		var rolling = new ShipState();
		rolling.stateName = "Rolling";
		rolling.canMove = false;
		rolling.canRotate = false;
		rolling.canDefend = false;
		rolling.timeTilRemoval = duration;
		return rolling;
	}

}


public class ShipState {
	public string stateName = "Normal";
	public bool canMove = true; //Thrust & brakes;
	public bool canRotate = true; //Rotation left Right
	public bool canDefend = true; //use defensive options
	public bool canShoot = true; //can shoot weapons
	public float timeTilRemoval = -1f;

}
