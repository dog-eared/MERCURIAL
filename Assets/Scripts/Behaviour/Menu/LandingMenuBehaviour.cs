using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingMenuBehaviour : MonoBehaviour {

	public Text planetName;
	public Text playerReputation;
	public Image planetSplashImage;

	public Button cantinaButton;
	public Button missionBoard;
	public Button tradeDepot;
	public Button mechanicsDepot;
	public Button refuelingDepot;

	public Text planetBlurb;

	public PilotData _playerPilotData;

	[Space(10)]
	public Sprite failedLoadImage;



	public void SetLandingData(SystemData systemData, Planet planetData) {
		planetName.text = planetData.planetName;
		planetBlurb.text = planetData.blurb;
		Debug.Log(planetBlurb.text);
		Debug.Log(planetData.blurb);
		playerReputation.text = SetReputationData(systemData.systemOwner);
		planetSplashImage.sprite = SetPlanetImage(systemData.systemName, planetData.planetName);
		SetPlanetButtons(planetData.buttons);
		Debug.Log("Done setting landing data!");

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
				 				"Reputation: " + _playerPilotData.rebelReputation;
			default:
				return "";
		}
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
