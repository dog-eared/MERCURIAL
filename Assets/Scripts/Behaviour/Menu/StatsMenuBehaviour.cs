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

	public GameObject longDescription; //Mission long text
	Text longText;

	public GameObject questList; //Used to assign new quests to correct gameobject in inspector

	[Header("Data Sources:")]
	public PilotData playerPilotData;
	public ShipData shipData;
	public MissionManager _missionManager;

	GameObject gameController;
	GameObject playerObject;
	MenuManager _menuManager;

	private bool missionsNeedUpdate = true; //Flag to check if missions should be redrawn

	void Awake() {
		reputationValuesText = reputationValues.GetComponent<Text>();
		pilotNameText = pilotName.GetComponent<Text>();
		callsignText = callsign.GetComponent<Text>();
		creditsText = credits.GetComponent<Text>();
		longText = longDescription.GetComponent<Text>();

		SetPlayerPilotData();
		//UpdatePlayerPilotData();
		Debug.Log("Complete");
	}


	void OnEnable() {

		SetPlayerPilotData();
		UpdatePlayerPilotData();

		List<Mission> missionList = new List<Mission>();

		if (missionsNeedUpdate) {

				foreach (Transform child in questList.transform) {
					Destroy(child.gameObject);
				}

				missionList = _missionManager.GetMissions();
				Debug.Log(missionList[0]);

				for (int i = 0; i < missionList.Count; i++) {
					GameObject newMission = Instantiate(Resources.Load("MenuPrefabs/IndividualElements/" + missionList[i].fileName), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
					newMission.transform.SetParent(questList.transform);
					newMission.transform.localScale = new Vector3(1, 1, 1);
				}

				missionsNeedUpdate = false;

		}

	}


	public void SelectMission(Mission mission) {
		longText.text = mission.longDescription + "\n" + mission.GetRewardsAsString();
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


	public void MissionListChanged() {
		missionsNeedUpdate = true;
	}


	public void AcceptButton() {
		_menuManager.CloseTopMenu();
	}


	public void CancelButton() {
		_menuManager.CloseTopMenu();
	}


}
