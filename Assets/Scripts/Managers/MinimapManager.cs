using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {

	/*
		MINIMAP MANAGER

		--Primarily handles the data needed to display an accurate minimap
		--Passes playerShip to minimap behaviour to update the positions of
		objects on the map.


	*/

	[Header("Minimap Config")]
	public float mapDistance = 50f;
	public bool showPlanetsOnEdge;

	[Space(10)]
	public float mapUpdateFrequency = 0.08f; //Base rate of map updating frequency
	public float shipMapQualityModifier = 2f; //Modifier based on how high quality the ship's map system is
	public float systemMapQualityModifier = 0f; //Modifier representing difficulty of getting info in current system

	[Space(10)]
	[Header("Prefabs/Refs")]
	public GameObject minimapObject;

	public GameObject planetBlip;
	public GameObject shipBlip;
	public SystemManager _systemManager;


	GameObject[] planetsArray;
	GameObject[] shipsList;
	ShipData[] shipsData;

	List<GameObject> minimapBlips = new List<GameObject>();


	public void GenerateSystemMap() {
		GetPlanetLocations();
	}

	void GetPlanetLocations() {
		planetsArray = GameObject.FindGameObjectsWithTag("Planet");
		GeneratePlanetBlips();
	}


	public void GeneratePlanetBlips() {
		//Debug.Log("Generating minimap planet blips");
		for (var planet = 0; planet < planetsArray.Length; planet++) {
			//Debug.Log(planetsArray[planet].name + " at " + planetsArray[planet].transform.position.x + " " + planetsArray[planet].transform.position.y);
			GameObject newBlip = Instantiate(planetBlip);
			newBlip.transform.SetParent(minimapObject.transform);
			newBlip.transform.localPosition = transformToMinimapPosition(planetsArray[planet].transform.position);
			newBlip.transform.localScale = scalePlanet(_systemManager.planetList[planet].minimapSize);
			minimapBlips.Add(newBlip);
		}
	}


	public void WipeAllBlips() {
		for (var x = (minimapBlips.Count - 1); x > -1; x--) {
			Debug.Log("removing" + minimapBlips[x]);
			GameObject.Destroy(minimapBlips[x]);
			minimapBlips.RemoveAt(x);
		}
		Debug.Log("All blips successfully wiped.");
	}


	void UpdateShipPositions() {

	}


	Vector3 scalePlanet(int planetScale) {
		//Return the proper scale based on the data found in PlanetData;
		return new Vector3(planetScale, planetScale, 1);
	}

	Vector3 transformToMinimapPosition(Vector3 ingamePosition) {
		//Simple return of normal value to improve readability in more important
		//methods.
		return new Vector3(ingamePosition.x, ingamePosition.y, 1);
	}

}
