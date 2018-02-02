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


	public void ShipWasKilled(GameObject ship, GameObject killer) {
		//If killer is player, sendMessage to mission control

	
		RemoveShip(ship);
	}


	public void AddNewShip(GameObject ship) {
		localShips.Add(ship);
	}

	void RemoveShip(GameObject ship) {
		//Specific ship
		localShips.Remove(ship);
	}

	public void RemoveAllShips() {
		//Public so that SystemManager can wipe this system.
		if (localShips.Count >= 1) {
			for (var x = localShips.Count - 1; x >= 0; x--) {
				Destroy(localShips[x].gameObject);
			}
		}
	}

}
