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

	public GameObject skillsValues;
	Text skillsValuesText;

	public GameObject callsign;
	Text callsignText;

	[Header("Data Sources:")]
	public PilotData playerPilotData;
	public ShipData shipData;



	void Awake() {
		skillsValuesText = skillsValues.GetComponent<Text>();
		pilotNameText = pilotName.GetComponent<Text>();
		callsignText = callsign.GetComponent<Text>();

		//UpdatePlayerPilotData();
		Debug.Log("Complete");
	}


	void OnEnable() {
		UpdatePlayerPilotData();
	}


	//defunct, but just incase we need to reload this at some point..
	public void SetPlayerPilotData(PilotData data) {
		if (playerPilotData == null) {
			playerPilotData = data;
		}
	}


	public void UpdatePlayerPilotData() {
		if (playerPilotData != null && shipData != null) {
			skillsValuesText.text = 				 playerPilotData.combatSkill
											+ "\n" + playerPilotData.diplomacySkill
											+ "\n" + playerPilotData.intimidationSkill
											+ "\n" + playerPilotData.mechanicsSkill
											+ "\n" + playerPilotData.thriftSkill;

			pilotNameText.text = "PILOT: " + playerPilotData.firstName + " " + playerPilotData.lastName;

			callsignText.text = playerPilotData.callsign + ", Captain of the " + shipData.shipName;

		} else {
			Debug.Log("Player pilot data could not be loaded!");
		}
	}


	public void AcceptButton() {
		Debug.Log("Accept pressed!");
	}


	public void CancelButton() {
		Debug.Log("Cancel pressed.");
	}


}
