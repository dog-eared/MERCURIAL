using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;



public class MenuManager : MonoBehaviour {

	/*
	MENU MANAGER

	--Handles which menus are open using a list (openMenus) and handles the graceful
	closing of each menu
	--OpenMenu methods return a bool (true) if successful -- this way we avoid
	lockouts where the menu can try to open, fail, and thereby lock the game.
	Right now we're going to see if having the CloseMenu methods not return any-
	thing is workable.


	TODO: Let's refactor using an enum or switch condition so we can cut down the
	number of functions now that we don't instantiate new menus!

	BETTER TODO: Let's do an enormous refactor of the whole thing

	*/

	SystemManager _systemManager;
	GameStateManager _gameStateManager;
	DockingBehaviour _dockingBehaviour;


	public GameObject landedMenuObject;
	public GameObject settingsMenuObject;
	public GameObject statsMenuObject;
	public GameObject galaxyMapObject;
	public GameObject guiStorage;

	bool settingsOpenedThisFrame = false;

	List<string> openMenus = new List<string>();

	void Awake() {
		_systemManager = GetComponent<SystemManager>();
		_gameStateManager = GetComponent<GameStateManager>();
		_dockingBehaviour = GetComponent<InputManager>().playerShip.GetComponent<DockingBehaviour>();
		openMenus.Add("Gameplay"); //Saving a headache by having our list always contain minimum count of 1
				//If we lose this string, the list will return a count of null, not 0 :|
	}


	public bool PlanetMenuOpen() {

		//Make sure you can't land with a menu open.
		if (openMenus[openMenus.Count - 1] != "Gameplay") {
			return false;
		}

		//First, check if we can find the proper json file...
		if (File.Exists(Application.dataPath + "/Resources/PlanetData/" + _systemManager.systemName + "/"
				+ _dockingBehaviour.planetTarget + ".json")) {
					landedMenuObject.SetActive(true);
					openMenus.Add("Landed Menu");
					_gameStateManager.SetGameMode("Menu");
					guiStorage.SetActive(false);
					return true;
		} else {
			Debug.Log("No file found!");
		}
		return false;
	}


	public bool SettingsMenuOpen() {
		//Activate the menu!
		settingsMenuObject.SetActive(true);
		openMenus.Add("Settings Menu");
		_gameStateManager.SetGameMode("Menu");
		return true;
}


	public bool GalaxyMenuOpen() {
		galaxyMapObject.SetActive(true);
		openMenus.Add("Galaxy Map");
		_gameStateManager.SetGameMode("Menu");
		Debug.Log("Success!");
		guiStorage.SetActive(false);
		return true;
	}


	public bool StatsMenuOpen() {
		//Activate the menu!
		statsMenuObject.SetActive(true);
		openMenus.Add("Stats Menu");
		_gameStateManager.SetGameMode("Menu");
		guiStorage.SetActive(false);
		return true;
	}


	public void CloseTopMenu() {

		if (openMenus.Count > 1) {
			string menuToClose = openMenus[openMenus.Count - 1];
			Debug.Log(menuToClose + " <-- unloading");

			/*
			Can we find a way to make menuToClose point at the variable we want to
			affect?

			ie something like this.menuToClose.SetActive(false);

			*/

			if (menuToClose == "Landed Menu") {
				landedMenuObject.SetActive(false);
			} else if (menuToClose == "Settings Menu") {
				settingsMenuObject.SetActive(false);
			} else if (menuToClose == "Stats Menu") {
				statsMenuObject.SetActive(false);
			} else if (menuToClose == "Galaxy Map") {
				galaxyMapObject.SetActive(false);
			}

			openMenus.RemoveAt((openMenus.Count - 1));

			if (openMenus.Count == 1) {
				_gameStateManager.SetGameMode("Normal");
				guiStorage.SetActive(true);
			}
		} else {
			Debug.Log("No menu to close.");
		}
	}

	void Update() {

		if (openMenus[openMenus.Count - 1] == "Gameplay" && Input.GetButtonDown("Cancel")) {
			SettingsMenuOpen();
			settingsOpenedThisFrame = true;
			guiStorage.SetActive(true);
		}

		if (Input.GetKeyDown("p")) {
			Debug.Log("Count:" + openMenus.Count);

			for (var x = 0; x < openMenus.Count; x++) {
				Debug.Log(x + " : " + openMenus[x]);
			}
		}

		if (Input.GetButtonDown("Land") && !(openMenus.Contains("Landing Menu")))  {
			PlanetMenuOpen();
		}

		if (Input.GetButtonDown("Stats") && !(openMenus.Contains("Stats Menu"))) {
			StatsMenuOpen();
		}

		if (Input.GetButtonDown("Map") && !(openMenus.Contains("Galaxy Map"))) {
			GalaxyMenuOpen();
		}

		if (openMenus[openMenus.Count - 1] != "Gameplay" && Input.GetButtonDown("Cancel") && !settingsOpenedThisFrame) {
				Debug.Log(openMenus[openMenus.Count - 1]);
				CloseTopMenu();
		}

		settingsOpenedThisFrame = false;

	}

}
