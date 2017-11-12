using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour {

	/*
		MINIMAP MANAGER

		--Primarily handles the data needed to display an accurate minimap



	*/

	[Header("Minimap Config")]
	public float mapDistance = 50f;
	public bool showPlanetsOnEdge;


	[Space(10)]
	public GameObject minimapObject;
	public GameObject planetBlip;
	public GameObject shipBlip;



	GameObject[] planetsArray;
	PlanetData[] planetData;

	GameObject[] shipsList;
	ShipData[] shipsData;


	public void GetPlanetLocations() {
		planetsArray = GameObject.FindGameObjectsWithTag("Planet");
		GeneratePlanetBlips();
	}

	public void GeneratePlanetBlips() {
		for (var planet = 0; planet < planetsArray.Length; planet++) {
			Debug.Log(planetsArray[planet].name + " at " + planetsArray[planet].transform.position.x + " " + planetsArray[planet].transform.position.y);
//			Instantiate(planetBlip, transform.position)
		}
	}

}
