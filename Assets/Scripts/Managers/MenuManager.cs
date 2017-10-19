using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuManager : MonoBehaviour {

	DockingBehaviour dockingBehaviour;
	public float gameTimeScale = 1f;
	public string gameState = "Normal";

	void Awake() {
		dockingBehaviour = GetComponent<InputManager>().playerShip.GetComponent<DockingBehaviour>();;
	}

	public bool PlanetMenuOpen() {

		if (dockingBehaviour.planetTarget == "Earth") {

			gameTimeScale = 0f;
			Time.timeScale = gameTimeScale;

			SceneManager.LoadScene("gui_staging", LoadSceneMode.Additive);
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

	public void PlanetMenuClose() {
		gameTimeScale = 1f;
		Time.timeScale = gameTimeScale;
		gameState = "Normal";

		SceneManager.UnloadSceneAsync("gui_staging");
	}


	void Update() {
		if (Input.GetKeyDown("l") && gameState == "Normal") {
			if (PlanetMenuOpen()) {
				gameState = "Visiting Planet";
			}
		}
	}

}
