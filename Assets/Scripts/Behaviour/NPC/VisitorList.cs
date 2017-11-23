using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorList : MonoBehaviour {
	/*VISITOR LIST

	Component to be attached to a blank Visitor List GameObject, which will be instantiated
	every time we jump into a new system. This script will handle:
		--Ships that are present when you enter a system
		--Ships that jump into or out of a system when there's no one to fight
		--Tagging ships as "Unique" which can only exist ONCE in a system

	*/
	[Header("Config:")]
	public int neutralsOnEntry = 0;
	public int maximumNeutrals = 10;

	[Header("Visitors:")]
	public Visitor[] neutrals;
	public Visitor[] pirates;

	List<GameObject> initialShips;

	[HideInInspector]
	public float chanceOfPirates; //Set by SystemManager;

	int instantiatedSoFar = 0;


	public void SpawnNeutrals() {
		initialShips = new List<GameObject>();
		if (neutrals.Length > 0) {

			for (var x = 0; x < neutrals.Length; x++) {

				for (var y = 0; y < neutrals[x].numberToInstantiate; y++) {

					if (instantiatedSoFar <= maximumNeutrals) {

						float randomCheck = Random.Range(0, 100);

						if (randomCheck > neutrals[x].appearanceFrequency) {
							Debug.Log("Instantiating a ship: " + neutrals[x].visitorPrefab);
							instantiatedSoFar++;
							GameObject newShip = Instantiate(neutrals[x].visitorPrefab);
							initialShips.Add(newShip);
							newShip.SetActive(false);
						}
					}
				}
			}
		}


	}


	public List<Vector3> GenerateSpawnLocations(List<Planet> planets, Vector3 playerLocation) {
		//Should be called by system manager.

		//Get all planets that the player is not exiting
		List<Vector3> returnedLocations = new List<Vector3>();

		for (int x = 0; x < planets.Count; x++) {

			returnedLocations.Add(new Vector3(planets[x].xLocation, planets[x].yLocation, 1));
			//if (planets[x].xLocation != playerLocation.x && planets[x].yLocation != playerLocation.y) {
			//}
		}
		Debug.Log("GenerateSpawnLocations -- I'm returning: " + returnedLocations + " " + returnedLocations.Count);
		return returnedLocations;
	}


	public void PlaceVisitors(List<Vector3> locations) {
		Debug.Log("starting PlaceVisitors");
		Debug.Log(initialShips + " count: " + initialShips.Count);
		if (initialShips.Count > 0) {

			for (int x = 0; x < initialShips.Count; x++) {
				int randomPlacement = Random.Range(0, locations.Count);
				Debug.Log("init ship pos: " + initialShips[x].transform.localPosition);
				Debug.Log("locations: " + locations[0]);
				initialShips[x].transform.localPosition = locations[randomPlacement];
				Debug.Log(initialShips[x].transform.position);
				initialShips[x].SetActive(true);
			}
		}
		Debug.Log("finished PlaceVisitors");
	}

}


[System.Serializable]
public class Visitor {
	public GameObject visitorPrefab;
	public bool isUnique = false;
	public float appearanceFrequency; //Should be between 0 and 100;
	public float numberToInstantiate;
}
