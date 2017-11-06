using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour {

	/*
		SYSTEM MANAGER

		--Handles data for current star system.
		--Contains methods for generation of star system background and planet placement.
		--Contains public data for how many asteroids to generate, chance of pirates, etc.

	*/

	//Data for the inspector:
	public string systemName;
	public Material backdropMaterial;
	public float systemSize;
	[Space(10)]
	public string systemOwner;
	public bool systemPopulated;
	[Space(10)]
	public int asteroidsToSpawn;
	public float chanceOfPirate;

	Camera mainCam;
	CameraBehaviour backdrop;
	AsteroidManager asteroids;
	PlanetManager planetManager;
	SystemData systemData;

	public GUIBehaviour _guiBehaviour;
	public GameObject systemBoundary;

	//SystemData should probably be in this file, not PlanetManager;
	//Refactor later?


	void Awake() {
		mainCam = Camera.main;
		backdrop = mainCam.GetComponent<CameraBehaviour>();
		asteroids = GetComponent<AsteroidManager>();
		planetManager = GetComponent<PlanetManager>();

		systemData = LoadSystemData("Sol");
	}


	public SystemData LoadSystemData(string target) {
		WipeCurrentSystem();

		TextAsset systemText = Resources.Load<TextAsset>("SystemData/" + target);
		SystemData data = JsonUtility.FromJson<SystemData>(systemText.text);

		SetSystemManager(data);

		for (var x = 0; x < data.planets.Count; x++) {
			Debug.Log("PlanetData/" + data.systemName + "/" +  data.planets[x]);
			TextAsset newPlanetText = Resources.Load<TextAsset>("PlanetData/" + data.systemName + "/" +  data.planets[x]);
			Planet newPlanet = JsonUtility.FromJson<Planet>(newPlanetText.text);
			planetManager.BuildPlanet(newPlanet);
		}


		_guiBehaviour.ReceiveMessage("Entered system: " + systemName, false);
		_guiBehaviour.ReceiveMessage("System owner: " + systemOwner, false);

		return data;

	}


	public void SetSystemManager(SystemData systemData) {

		//Set all system data here
		//Can I refactor to simplify??
		systemName = systemData.systemName;
		backdropMaterial = Resources.Load("Visuals/Backdrops/" + systemData.backdropMaterial, typeof(Material)) as Material;
		systemSize = systemData.systemSize;
		systemOwner = systemData.systemOwner;
		systemPopulated = systemData.systemPopulated;
		asteroidsToSpawn = systemData.asteroidsToSpawn;
		chanceOfPirate = systemData.chanceOfPirate;

		InitializeArea();
	}


	void WipeCurrentSystem() {
		GameObject[] toWipe = GameObject.FindGameObjectsWithTag("Asteroid");
		WipeObjects(toWipe);
		toWipe = GameObject.FindGameObjectsWithTag("Planet");
		WipeObjects(toWipe);
		toWipe = GameObject.FindGameObjectsWithTag("SystemEdge");
		WipeObjects(toWipe);
	}


	void WipeObjects(GameObject[] toWipe) {
		for (var x = 0; x < toWipe.Length; x++) {
			Destroy(toWipe[x]);
		}
	}


	void InitializeArea() {
		backdrop.DisplayBackdrop(backdropMaterial);
		GenerateBoundingEdges();
		asteroids.SpawnAsteroids(asteroidsToSpawn, systemSize);
	}


	void GenerateBoundingEdges() {
		GameObject newBoundary = Instantiate(systemBoundary, new Vector3 (0, -systemSize, 0), Quaternion.identity);
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);

		newBoundary = Instantiate(systemBoundary, new Vector3 (0, systemSize, 0), Quaternion.identity);
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);

		newBoundary = Instantiate(systemBoundary, new Vector3 (systemSize, 0, 0), Quaternion.Euler(0,0,90));
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);

		newBoundary = Instantiate(systemBoundary, new Vector3 (-systemSize, 0, 0), Quaternion.Euler(0,0,90));
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);
	}

}


/*
FOR INFO ABOUT SystemData, see PLANET MANAGER!

*/
