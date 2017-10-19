using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {

	/*
		MINIMAP MANAGER

		--Primarily handles the data needed to display an accurate MINIMAP
		--Keeps all planet and ship data for purposes of figuring out what data to
		pull when the player tries to communicate or interact (in ways other than
		combat)



	*/

	GameObject[] planetsList;
	PlanetData[] planetsData;

	GameObject[] shipsList;
	ShipData[] shipsData;

	void GetAllPlanets() {
		planetsList = GameObject.FindGameObjectsWithTag("Planet");

		planetsData = new PlanetData[planetsList.Length];

		for (var x = 0; x < planetsData.Length; x++) {
			planetsData[x] = planetsList[x].GetComponent<PlanetData>();
		}
	}

	void Awake() {
		GetAllPlanets();
	}

	void Update() {
		if (Input.GetKeyDown("x")) {
			Debug.Log("All planet names:");
			for (var z = 0; z < planetsData.Length; z++) {
				Debug.Log("Index: " + z + "  |  Name: " + planetsData[z].planetName);
			}
		}
	}

}
