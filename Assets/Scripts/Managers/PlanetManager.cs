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



[System.Serializable]
public class SystemData {
	//Simple container that stores our array of planets, meaning we can access
	//them using dot notation.

	//Is also passed back to SystemManager so System information can be worked in

	public string systemName;
	public string backdropMaterial; //Important this string, not Material. It will be cast as a material at assignment to PlanetData;
	public string systemOwner;
	public bool systemPopulated;
	public float systemSize;
	public int asteroidsToSpawn;
	public float chanceOfPirate;

	public List<string> planets;
}

[System.Serializable]
public class Planet {
	//Each piece of data we use MUST be defined in this class.
	public string planetName = "Unknown Planet";
	public int minimapSize;
	public float xLocation;
	public float yLocation;
	public float scale;
	public string mapGraphic;
	//Data for landing:
	public string landingGraphic;
	public List<bool> buttons;
	//public string blurb;


}
