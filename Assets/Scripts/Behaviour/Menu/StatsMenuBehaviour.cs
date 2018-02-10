using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenuBehaviour : MonoBehaviour {
	/*
	STATS MENU BEHAVIOUR

	--Handles updating menu stats to be accurate
	--Store functions for buttons
	--Is passed the PlayerStats/ShipStats objects to update data.
	--Stores references to various parts of screen that update; stats, name,
	reputations, etc.

	*/

	[Header("References:")]
	public GameObject pilotName;
	Text pilotNameText;

	public GameObject reputationValues;
	Text reputationValuesText;

	public GameObject callsign;
	Text callsignText;

	public GameObject credits;
	Text creditsText;

	[Header("Data Sources:")]
	public PilotData playerPilotData;
	public ShipData shipData;

	GameObject gameController;
	GameObject playerObject;
	MenuManager _menuManager;
	MissionManager _missionManager;

	void Awake() {
		reputationValuesText = reputationValues.GetComponent<Text>();
		pilotNameText = pilotName.GetComponent<Text>();
		callsignText = callsign.GetComponent<Text>();
		creditsText = credits.GetComponent<Text>();
		SetPlayerPilotData();
		//UpdatePlayerPilotData();
		Debug.Log("Complete");
	}


	void OnEnable() {
		SetPlayerPilotData();
		UpdatePlayerPilotData();
	}


	//defunct, but just incase we need to reload this at some point..
	public void SetPlayerPilotData() {
		if (playerPilotData == null) {
			gameController = GameObject.FindGameObjectWithTag("GameController");
			playerObject = GameObject.FindGameObjectWithTag("PlayerObject");
			_menuManager = gameController.GetComponent<MenuManager>();
			playerPilotData = playerObject.GetComponent<PilotData>();
			shipData = playerObject.transform.GetChild(0).GetComponent<ShipData>();
		}
	}


	public void UpdatePlayerPilotData() {
		if (playerPilotData != null && shipData != null) {

			creditsText.text = playerPilotData.credits + " credits.";

			reputationValuesText.text =  playerPilotData.dominionReputation
											+ "\n" + playerPilotData.allianceReputation
											+ "\n" + playerPilotData.rebellionReputation
											+ "\n" + playerPilotData.combatRating
											+ "\n" + playerPilotData.karma;

			pilotNameText.text = "PILOT: " + playerPilotData.firstName + " " + playerPilotData.lastName;

			callsignText.text = playerPilotData.callsign + ", Captain of the " + shipData.shipName;

		} else {
			Debug.Log("Player pilot data could not be loaded!");
		}



	}


	public void AcceptButton() {
		_menuManager.CloseTopMenu();
	}


	public void CancelButton() {
		_menuManager.CloseTopMenu();
	}


}
