using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameData : MonoBehaviour {

	/*	PERSISTENT GAME DATA

	Handles persistent game data -- what has the player accomplished, what's going
	on various planets, where has the player been, what day is it, etc.

	Does NOT include data as it's meant to be seen by players -- we don't care
	about player stats like Intimidation, or reputations like Dominion Rep here.
	This data will handle anything that needs to persist between scene loads (due
	to going to/from main menu or launching from a planet).


	*/

	PilotData _playerPilotData;
	ShipData _playerShipData;

	public string currentSystem;
	public string currentPlanet;

	public GameObject _gameManager;

	//Initial values for date.
	//int day = 1;
	//int month = 10;
	//int year = 2717;


	void Awake() {
		Object.DontDestroyOnLoad(this.gameObject);
	}

	public void NewGame() {
		currentSystem = "Sol";
		currentPlanet = "Earth";
		SceneManager.LoadScene("staging_4");

	}

}
