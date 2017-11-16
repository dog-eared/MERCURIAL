using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;


public class PlanetManager : MonoBehaviour {

	/*
	PLANET MANAGER

	Subordinate to our SystemManager class.

	--Handles reading of .json data from our gameDataFileName variable, which is
	set by SystemManager when we change systems and used by our LoadPlanetData
	function which instantiates all of our planets.
	--

	*/


	public GameObject planetPrefab;



	public void BuildPlanet(Planet planetToBuild) {
		GameObject newPlanet = Instantiate(planetPrefab, new Vector3(planetToBuild.xLocation, planetToBuild.xLocation, 6), Quaternion.identity);
		PlanetData newPlanetData = newPlanet.GetComponent<PlanetData>();

		newPlanet.transform.localScale = new Vector3(planetToBuild.scale, planetToBuild.scale, 1);

		newPlanetData.planetName = planetToBuild.planetName;
		newPlanetData.mapGraphic = planetToBuild.mapGraphic;
		newPlanetData.buttons = planetToBuild.buttons;

		newPlanetData.UpdatePlanetDisplay();
	}


}
