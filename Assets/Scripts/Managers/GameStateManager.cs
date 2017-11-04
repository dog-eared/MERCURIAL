using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {

	public GameMode currentMode;

	public float combatGameSpeed = 0.8f;
	public float normalGameSpeed = 1f;
	public float fastGameSpeed = 1.5f;

	float speedMultiplier;

	public bool fastModeOn = false;

	CameraBehaviour _cameraBehaviour;

	void Awake() {
		_cameraBehaviour = Camera.main.GetComponent<CameraBehaviour>();
	}


	void HyperspaceJump(string targetSystem, int direction) {
		//
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

}



public enum GameMode {
	menu,
	normal,
	combat,
	hyperspace
}
