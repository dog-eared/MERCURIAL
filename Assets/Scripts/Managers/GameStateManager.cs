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

	void Awake() {
		_cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
		_systemManager = GetComponent<SystemManager>();
	}


	void HyperspaceJump(string targetSystem, int direction) {
		_systemManager.LoadSystemData(targetSystem);
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
		_cameraBehaviour.StartCoroutine("HyperspaceFadeIn");
	}


	void Update() {

		if (Input.GetKeyDown("x")) {
			_cameraBehaviour.StartCoroutine("HyperspaceFadeOut");
		}

		if (_cameraBehaviour.hyperspaceLerp >= 1 && _systemManager.systemName != targetSystem) {
			HyperspaceJump(targetSystem, 2);
			Debug.Log("jumped!");
			Invoke("StartFadeIn", 0); //See FIXME above.
		}

		else if (Input.GetKeyDown("c")) {
			_cameraBehaviour.StartCoroutine("HyperspaceFadeIn");
		}

	}

}



public enum GameMode {
	menu,
	normal,
	combat,
	hyperspace
}
