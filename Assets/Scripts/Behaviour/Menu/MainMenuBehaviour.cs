using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour {

	public PersistentGameData _persistantData;

	public void NewGameButton() {
		_persistantData.NewGame();
	}

	public void SettingsButton() {
		Debug.Log("Settings");
	}

	public void ExitGameButton() {
		Application.Quit();
	}

}
