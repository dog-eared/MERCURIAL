using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStateBehaviour : MonoBehaviour {

	int top = 0;
	List<string> shipState = new List<string>();

	void Awake() {
		shipState.Add("NormalState");
	}

	public string GetTopState() {
		return shipState[top];
	}

	public void AddState(string newState) {
		if (!(shipState.Contains(newState))) {
			shipState.Add(newState);
			top = shipState.Count;
		}
	}

	public void PopTopState() {
		shipState.RemoveAt(shipState.Count - 1);
		top = shipState.Count;
	}

}
