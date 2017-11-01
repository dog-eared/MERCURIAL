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

	public GameObject experiences;
	List<string> experiencesList;


	[Header("Data Sources:")]
	public PilotData playerPilotData;
	public ShipData shipData;

	GameObject gameController;
	GameObject playerObject;
	MenuManager menuManager;

	void Awake() {
		skillsValuesText = skillsValues.GetComponent<Text>();
		pilotNameText = pilotName.GetComponent<Text>();
		callsignText = callsign.GetComponent<Text>();
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
			menuManager = gameController.GetComponent<MenuManager>();
			playerPilotData = playerObject.GetComponent<PilotData>();
			shipData = playerObject.transform.GetChild(0).GetComponent<ShipData>();
		}
	}


	public void UpdatePlayerPilotData() {
		if (playerPilotData != null && shipData != null) {
			skillsValuesText.text =  playerPilotData.combatSkill
											+ "\n" + playerPilotData.diplomacySkill
											+ "\n" + playerPilotData.intimidationSkill
											+ "\n" + playerPilotData.mechanicsSkill
											+ "\n" + playerPilotData.thriftSkill;

			pilotNameText.text = "PILOT: " + playerPilotData.firstName + " " + playerPilotData.lastName;

			callsignText.text = playerPilotData.callsign + ", Captain of the " + shipData.shipName;

			experiencesList = playerPilotData.pilotExperiences;

			/*
			TO FIX: This is a bit heavy, destroying/recreating the tag list each time.
			Later, we can refactor so this doesn't happen every time you open/close
			the menu. For now, performance isn't a big enough issue to worry.

			Even when done, a character probably won't ever have 20+ tags so maybe its
			fine
			*/
			foreach (Transform experience in experiences.transform) {
				GameObject.Destroy(experience.gameObject);
			}

			for (var x = 0; x < experiencesList.Count; x++) {
				GameObject experiencePrefab = Resources.Load("MenuPrefabs/IndividualElements/" + experiencesList[x]) as GameObject;
				Instantiate(experiencePrefab, new Vector3(0, 0, 0), Quaternion.identity, experiences.transform);
			}

		} else {
			Debug.Log("Player pilot data could not be loaded!");
		}



	}


	public void AcceptButton() {
		menuManager.CloseTopMenu();
	}


	public void CancelButton() {
		menuManager.CloseTopMenu();
	}


}
