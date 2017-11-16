using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	/* GAME STATE MANAGER

	Main manager of game data. Contains:
	--Game Speed and controls for setting game Speed
	--Controls for saving/loading data (WIP)
	--Flags for events triggered and universe settings (WIP)


	*/


	public GameMode currentMode;

	public float combatGameSpeed = 0.8f;
	public float normalGameSpeed = 1f;
	public float fastGameSpeed = 1.5f;

	public string targetSystem;

	float speedMultiplier;

	public bool fastModeOn = false;

	CameraBehaviour _cameraBehaviour;
	SystemManager _systemManager;
	MinimapManager _minimapManager;

	void Awake() {
		_cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
		_systemManager = GetComponent<SystemManager>();
		_minimapManager = GetComponent<MinimapManager>();
		targetSystem = "";
	}


	public void HyperspaceJump() {
		_systemManager.systemData = _systemManager.LoadSystemData(targetSystem);
		_minimapManager.Invoke("GenerateSystemMap", 0.5f); //Gives a moment for json data to load
		StartFadeIn();
	}



	public void SetGameMode(string mode) {
		if (fastModeOn) {
			speedMultiplier = fastGameSpeed;
		} else {
			speedMultiplier = 1;
		}

		if (mode == "Menu" || mode == "Hyperspace") {
			currentMode = GameMode.menu;
			Time.timeScale = 0f;
		} else if (mode == "Normal") {
			currentMode = GameMode.normal;
			Time.timeScale = normalGameSpeed * speedMultiplier;
		} else if (mode == "Combat") {
			currentMode = GameMode.combat;
			Time.timeScale = combatGameSpeed * speedMultiplier;
		}
		Debug.Log(Time.timeScale);
	}


	//FIXME: Ugly, clunky solution. Starting the coroutine after system load by
	//having it called directly in update is not working. Instead, invoking with
	//a time of 0 seconds seems to be the solution. Research and fix later.
	void StartFadeIn() {
		_cameraBehaviour.StartCoroutine("BackdropFadeIn");
	}


	/*void Update() {
		//To be replaced later
		if (Input.GetKeyDown("x")) {
			Debug.Log(targetSystem);
			if (targetSystem != "" && targetSystem != _systemManager.systemName) {
				_cameraBehaviour.StartCoroutine("BackdropFadeOut");
			}
		}

		if (_cameraBehaviour.fadeLerp >= 1 && _systemManager.systemName != targetSystem) {
			HyperspaceJump(targetSystem, 2);
			Debug.Log("jumped!");
			Invoke("StartFadeIn", 0); //See FIXME above.
		}
	}*/

}



public enum GameMode {
	menu,
	normal,
	combat,
	hyperspace
}
