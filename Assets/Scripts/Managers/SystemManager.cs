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
	public SystemData systemData;
	public string systemName;
	public Material backdropMaterial;
	public float systemSize;
	[Space(10)]
	public string systemOwner;
	public bool systemPopulated;
	[Space(10)]
	public GameObject visitorList;

	public int asteroidsToSpawn;
	public float chanceOfPirate;

	Camera mainCam;
	CameraBehaviour backdrop;
	AsteroidManager asteroids;
	PlanetManager planetManager;

	public GUIBehaviour _guiBehaviour;
	public MinimapManager _minimapManager;
	public LocalShipsManager _localShipsManager;
	public MissionManager _missionManager;

	public GameObject systemBoundary;
	public GameObject systemBoundaryContainer;

	public List<Planet> planetList;

	public VisitorList _visitorList;

	List<Vector3> spawnLocations = new List<Vector3>();

	void Awake() {
		mainCam = Camera.main;
		backdrop = mainCam.GetComponent<CameraBehaviour>();
		asteroids = GetComponent<AsteroidManager>();
		planetManager = GetComponent<PlanetManager>();

		systemData = LoadSystemData("Sol");
		_minimapManager.GenerateSystemMap();
	}


	public SystemData LoadSystemData(string target) {

		WipeCurrentSystem();

		TextAsset systemText = Resources.Load<TextAsset>("SystemData/" + target);
		SystemData data = JsonUtility.FromJson<SystemData>(systemText.text);

		SetSystemManager(data);

		for (var x = 0; x < data.planets.Count; x++) {

			TextAsset newPlanetText = Resources.Load<TextAsset>("PlanetData/" + data.systemName + "/" +  data.planets[x]);
			Planet newPlanet = JsonUtility.FromJson<Planet>(newPlanetText.text);
			planetList.Add(newPlanet);
			planetManager.BuildPlanet(newPlanet);
		}

		InitializeOtherShips();

		_guiBehaviour.ReceiveMessage("Entered system: " + systemName, false);
		_guiBehaviour.ReceiveMessage("System owner: " + systemOwner, false);

		Invoke("CheckArea", 0.75f);

		return data;

	}


	public void SetSystemManager(SystemData systemData) {

		//Set all system data here
		//Can I refactor to simplify??
		systemName = systemData.systemName;
		backdropMaterial = Resources.Load("Visuals/Backdrops/" + systemData.backdropMaterial, typeof(Material)) as Material;
		visitorList = Resources.Load("Prefabs/Visitor List/" + systemData.visitorList) as GameObject;

		_visitorList = visitorList.GetComponent<VisitorList>();

		systemSize = systemData.systemSize;
		systemOwner = systemData.systemOwner;
		systemPopulated = systemData.systemPopulated;
		asteroidsToSpawn = systemData.asteroidsToSpawn;
		chanceOfPirate = systemData.chanceOfPirate;

		InitializeArea();

	}


	public void LeavePlanet() {
		GameObject[] toWipe = GameObject.FindGameObjectsWithTag("Asteroid");
		WipeObjects(toWipe);

		_localShipsManager.RemoveAllShips();

		InitializeOtherShips();
	}


	void WipeCurrentSystem() {

		GameObject[] toWipe = GameObject.FindGameObjectsWithTag("Asteroid");
		WipeObjects(toWipe);
		toWipe = GameObject.FindGameObjectsWithTag("Planet");
		WipeObjects(toWipe);
		planetList = new List<Planet>();
		toWipe = GameObject.FindGameObjectsWithTag("SystemEdge");
		WipeObjects(toWipe);


		_localShipsManager.RemoveAllShips();
		_minimapManager.WipeAllBlips();
	}


	void WipeObjects(GameObject[] toWipe) {
		for (var x = 0; x < toWipe.Length; x++) {
			Destroy(toWipe[x]);
		}
	}


	void InitializeArea() {
		backdrop.DisplayBackdrop(backdropMaterial);
		GenerateBoundingEdges();

		//Instantiate(visitorList);

		asteroids.SpawnAsteroids(asteroidsToSpawn, systemSize);
	}


	void InitializeOtherShips() {
		_localShipsManager.localShips = _visitorList.SpawnNeutrals();

		spawnLocations = _visitorList.GenerateSpawnLocations(planetList, new Vector3(0, 0, 1));

		_visitorList.PlaceVisitors(spawnLocations);
	}


	void CheckArea() {
		_missionManager.CheckNewArea(systemName);
	}


	void GenerateBoundingEdges() {
		//This is just for my own peace of mind, so the hierarchy stays clean
		GameObject container = Instantiate(systemBoundaryContainer, new Vector3(0, 0, 0), Quaternion.identity);

		//TODO: Refactor to be DRY

		GameObject newBoundary = Instantiate(systemBoundary, new Vector3 (0, -systemSize, 0), Quaternion.identity);
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);
		newBoundary.transform.SetParent(container.transform);

		newBoundary = Instantiate(systemBoundary, new Vector3 (0, systemSize, 0), Quaternion.identity);
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);
		newBoundary.transform.SetParent(container.transform);

		newBoundary = Instantiate(systemBoundary, new Vector3 (systemSize, 0, 0), Quaternion.Euler(0,0,90));
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);
		newBoundary.transform.SetParent(container.transform);

		newBoundary = Instantiate(systemBoundary, new Vector3 (-systemSize, 0, 0), Quaternion.Euler(0,0,90));
		newBoundary.transform.localScale = new Vector3(systemSize, 1, 0);
		newBoundary.transform.SetParent(container.transform);

	}


}
