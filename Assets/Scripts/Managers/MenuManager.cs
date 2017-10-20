using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour {

	/*
	MENU MANAGER

	--Handles which menus are open using a list (openMenus) and handles the graceful
	closing of each menu
	--OpenMenu methods return a bool (true) if successful -- this way we avoid
	lockouts where the menu can try to open, fail, and thereby lock the game.
	Right now we're going to see if having the CloseMenu methods not return any-
	thing is workable.


	*/

	DockingBehaviour dockingBehaviour;
	public float gameTimeScale = 1f;
	public string gameState = "Normal";

	string landingMenu = "MenuScenes/landed_menu";
	string settingsMenu = "MenuScenes/settings_menu";
	string statsMenu = "MenuScenes/stats_menu";

	List<string> openMenus = new List<string>();

	void Awake() {
		dockingBehaviour = GetComponent<InputManager>().playerShip.GetComponent<DockingBehaviour>();
		openMenus.Add("Gameplay"); //Saving a headache by having our list always contain minimum count of 1
				//If we lose this string, the list will return a count of null, not 0 :|
	}

	public bool PlanetMenuOpen() {

		if (dockingBehaviour.planetTarget == "Earth") {
			Time.timeScale = 0;

			SceneManager.LoadScene(landingMenu, LoadSceneMode.Additive);
			openMenus.Add(landingMenu);
			Debug.Log(openMenus[openMenus.Count - 1]);
			return true;

		} else if (dockingBehaviour.planetTarget == "Mars") {
			Debug.Log("Landing on Mars is ill advised because the surface is shite");
		} else if (dockingBehaviour.planetTarget == "Moon") {
			Debug.Log("The moon isn't worth docking on");
		} else {
			Debug.Log("Nowhere to land!");
		}
		return false;
	}


	public bool SettingsMenuOpen() {
		Time.timeScale = 0;

		SceneManager.LoadScene(settingsMenu, LoadSceneMode.Additive);
		openMenus.Add(settingsMenu);
		Debug.Log(openMenus[openMenus.Count - 1]);
		return true;
	}


	public bool StatsMenuOpen() {
		if (!(openMenus.Contains(statsMenu))) {
			Time.timeScale = 0;

			SceneManager.LoadScene(statsMenu, LoadSceneMode.Additive);
			openMenus.Add(statsMenu);
			Debug.Log(openMenus[openMenus.Count - 1]);
			return true;
		}
		return false;
	}


	public void CloseTopMenu() {
		if (openMenus.Count != 1) {
			string sceneToUnload = openMenus[openMenus.Count - 1];
			Debug.Log(sceneToUnload + " <-- unloading");
			SceneManager.UnloadScene(openMenus[openMenus.Count - 1]);
			openMenus.RemoveAt((openMenus.Count - 1));
			Debug.Log("Count is now: " + openMenus.Count);

			if (openMenus.Count == 1) {
				Time.timeScale = gameTimeScale;
			}
		} else {
			Debug.Log("No menu to close.");
		}
	}

	void Update() {

		if (openMenus[openMenus.Count - 1] == "Gameplay" && Input.GetButtonDown("Cancel")) {
			Debug.Log(openMenus.Contains(settingsMenu));
			SettingsMenuOpen();
		}
		
		if (Input.GetKeyDown("p")) {
			Debug.Log("Count:" + openMenus.Count);

			for (var x = 0; x < openMenus.Count; x++) {
				Debug.Log(x + " : " + openMenus[x]);
			}
		}

		if (Input.GetKeyDown("l") && !(openMenus.Contains(landingMenu)))  {
			Debug.Log(openMenus.Contains(landingMenu));
			PlanetMenuOpen();
		}



		if (Input.GetKeyDown("i") && !(openMenus.Contains(statsMenu))) {
			Debug.Log(openMenus.Contains(statsMenu));
			StatsMenuOpen();
		}

		if (openMenus[openMenus.Count - 1] != "Gameplay" && Input.GetButtonDown("Cancel")) {
				Debug.Log(openMenus[openMenus.Count - 1]);
				CloseTopMenu();
		}


	}

}
