using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalShipsManager : MonoBehaviour {

	public List<GameObject> localShips;

	public void AllAggroTarget(GameObject offender, string faction) {
		for (int x = 0; x < localShips.Count; x++) {
			if (localShips[x] != null && localShips[x].tag == faction) {
				localShips[x].SendMessage("AllyWasHit", offender);
			}
		}
	}


	public void AddNewShip(GameObject ship) {
		localShips.Add(ship);
	}


	public void RemoveAllShips() {
		if (localShips.Count >= 1) {
			for (var x = localShips.Count - 1; x >= 0; x--) {
				Destroy(localShips[x].gameObject);
			}
		}
	}

}
