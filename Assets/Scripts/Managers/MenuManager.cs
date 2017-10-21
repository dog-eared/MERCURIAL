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


	*/

	SystemManager systemManager;

	DockingBehaviour dockingBehaviour;
	public float gameTimeScale = 1f;
	public string gameState = "Normal";

	string landingMenu = "MenuScenes/landed_menu";
	string settingsMenu = "MenuScenes/settings_menu";
	string statsMenu = "MenuScenes/stats_menu";

	public GameObject landedMenuObject;
	public GameObject settingsMenuObject;
	public GameObject statsMenuObject;

	bool settingsOpenedThisFrame = false;

	List<string> openMenus = new List<string>();

	void Awake() {
		systemManager = GetComponent<SystemManager>();
		dockingBehaviour = GetComponent<InputManager>().playerShip.GetComponent<DockingBehaviour>();
		openMenus.Add("Gameplay"); //Saving a headache by having our list always contain minimum count of 1
				//If we lose this string, the list will return a count of null, not 0 :|

		LoadMenuScenes();
	}


	public void LoadMenuScenes() {

		settingsMenuObject = Instantiate(settingsMenuObject as GameObject, new Vector3(0, 0, 5), Quaternion.identity);
		settingsMenuObject.name = "SettingsMenu";

		landedMenuObject = Instantiate(landedMenuObject as GameObject, new Vector3(0, 0, 5), Quaternion.identity);
		landedMenuObject.name = "LandedMenu";

		statsMenuObject = Instantiate(statsMenuObject as GameObject, new Vector3(0, 0, 5), Quaternion.identity);
		statsMenuObject.name = "StatsMenu";

		settingsMenuObject.SetActive(false);
		landedMenuObject.SetActive(false);
		statsMenuObject.SetActive(false);

	}

	public bool PlanetMenuOpen() {

		//Make sure you can't land with a menu open.
		if (openMenus[openMenus.Count - 1] != "Gameplay") {
			return false;
		}

		//First, check if we can find the proper json file...
		if (File.Exists(Application.dataPath + "/Resources/PlanetData/" + systemManager.systemName + "/"
				+ dockingBehaviour.planetTarget + ".json")) {
					landedMenuObject.SetActive(true);
					openMenus.Add("landedMenuObject");
					Time.timeScale = 0;
					return true;
				} else {
					Debug.Log("No file found!");
				}
		return false;
	}


	public bool SettingsMenuOpen() {
		//Activate the menu!
		settingsMenuObject.SetActive(true);
		openMenus.Add("settingsMenuObject");
		Time.timeScale = 0;
		return true;
}


	public bool StatsMenuOpen() {
		//Activate the menu!
		statsMenuObject.SetActive(true);
		openMenus.Add("statsMenuObject");
		Time.timeScale = 0;
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

			if (menuToClose == "landedMenuObject") {
				landedMenuObject.SetActive(false);
			} else if (menuToClose == "settingsMenuObject") {
				settingsMenuObject.SetActive(false);
			} else if (menuToClose == "statsMenuObject") {
				statsMenuObject.SetActive(false);
			}

			openMenus.RemoveAt((openMenus.Count - 1));

			if (openMenus.Count == 1) {
				Time.timeScale = gameTimeScale;
			}
		} else {
			Debug.Log("No menu to close.");
		}
	}

	void Update() {

		if (openMenus[openMenus.Count - 1] == "Gameplay" && Input.GetButtonDown("Cancel")) {
			SettingsMenuOpen();
			settingsOpenedThisFrame = true;
		}

		if (Input.GetKeyDown("p")) {
			Debug.Log("Count:" + openMenus.Count);

			for (var x = 0; x < openMenus.Count; x++) {
				Debug.Log(x + " : " + openMenus[x]);
			}
		}

		if (Input.GetButtonDown("Land") && !(openMenus.Contains(landingMenu)))  {
			PlanetMenuOpen();
		}

		if (Input.GetButtonDown("Stats") && !(openMenus.Contains(statsMenu))) {
			StatsMenuOpen();
		}

		if (openMenus[openMenus.Count - 1] != "Gameplay" && Input.GetButtonDown("Cancel") && !settingsOpenedThisFrame) {
				Debug.Log(openMenus[openMenus.Count - 1]);
				CloseTopMenu();
		}

		settingsOpenedThisFrame = false;

	}

}
