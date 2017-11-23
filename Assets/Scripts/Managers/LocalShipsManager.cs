using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalShipsManager : MonoBehaviour {

	public List<GameObject> localShips;

	public void WipeAllShips() {
		if (localShips.Count >= 1) {
			for (var x = localShips.Count - 1; x >= 0; x--) {
				Destroy(localShips[x].gameObject);
			}
		}
	}

}
