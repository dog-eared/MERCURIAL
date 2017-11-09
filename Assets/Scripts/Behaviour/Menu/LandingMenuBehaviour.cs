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


	public void SetLandingData(SystemData systemData, Planet planetData) {
		planetName.text = planetData.planetName;
		planetBlurb.text = planetData.blurb;

		SetReputationData(systemData.systemOwner);
	}

	string SetReputationData(string owner) {
		switch (owner) {
			case "Dominion of Earth":
				return "Reputation: " + _playerPilotData.dominionReputation + "\n" +
							 "Faction: Dominion of Earth";
			case "Divine Trade Alliance":
				return "Reputation: " + _playerPilotData.allianceReputation + "\n" +
						 "Faction: Divine Trade Alliance";
			default:
				return "";
		}

		if (owner == "Dominion of Earth") {

		}
	}



}
