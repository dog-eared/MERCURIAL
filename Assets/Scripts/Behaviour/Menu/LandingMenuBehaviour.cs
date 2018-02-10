using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingMenuBehaviour : MonoBehaviour {

	public Text planetName;
	public Text playerReputation;
	public Image planetSplashImage;

	[Space(10)]
	public Button cantinaButton;
	public Button missionBoard;
	public Button tradeDepot;
	public Button mechanicsDepot;
	public Button refuelingDepot;
	public Text planetBlurb;

	[Space(10)]
	public SystemManager _systemManager;
	public GameObject playerObject;
	GameObject _playerShip;
	PilotData _playerPilotData;

	[Space(10)]
	public Sprite failedLoadImage;

	Planet currentPlanet;

	void Awake() {
		_playerPilotData = playerObject.GetComponent<PilotData>();
		_playerShip = playerObject.transform.GetChild(0).gameObject;
	}


	public void SetLandingData(SystemData systemData, Planet planetData) {
		planetName.text = planetData.planetName;
		planetBlurb.text = planetData.blurb;
		Debug.Log(planetBlurb.text);
		Debug.Log(planetData.blurb);
		playerReputation.text = SetReputationData(systemData.systemOwner);
		planetSplashImage.sprite = SetPlanetImage(systemData.systemName, planetData.planetName);
		SetPlanetButtons(planetData.buttons);
		Debug.Log("Done setting landing data!");

		currentPlanet = planetData;
	}


	void SetPlanetButtons(List<bool> buttonsList) {
		cantinaButton.gameObject.SetActive(buttonsList[0]);
		missionBoard.gameObject.SetActive(buttonsList[1]);
		tradeDepot.gameObject.SetActive(buttonsList[2]);
		mechanicsDepot.gameObject.SetActive(buttonsList[3]);
		refuelingDepot.gameObject.SetActive(buttonsList[4]);
	}


	string SetReputationData(string owner) {
		Debug.Log("Setting planet owner as " + owner);
		switch (owner) {
			case "Dominion of Earth":
				return "Faction: Dominion of Earth \n" +
				 				"Reputation: " + _playerPilotData.dominionReputation;
			case "Divine Trade Alliance":
				return "Faction: Divine Trade Alliance \n" +
								"Reputation: " + _playerPilotData.allianceReputation;
			case "Rebellion":
				return "Faction: Rebellion \n" +
				 				"Reputation: " + _playerPilotData.rebellionReputation;
			default:
				return "";
		}
	}


	public void LeavePlanet() {

		//This lets us remove any velocity without needing access to a rigidbody
		_playerShip.SetActive(false);
		_playerShip.SetActive(true);
		//
		_playerShip.transform.position = new Vector3(currentPlanet.xLocation, currentPlanet.yLocation, 3);

		_systemManager.LeavePlanet();

	}


	Sprite SetPlanetImage(string systemName, string planetName) {
		Sprite returnImage = Resources.Load("Visuals/StationMenu/" + systemName + "/" + planetName + "_Splash", typeof(Sprite)) as Sprite;

		if (returnImage != null) {
			return returnImage;
		} else {
			return failedLoadImage;
		}

	}



}
