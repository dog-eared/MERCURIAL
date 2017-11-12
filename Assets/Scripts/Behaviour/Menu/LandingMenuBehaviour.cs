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
		Debug.Log(planetBlurb.text);
		Debug.Log(planetData.blurb);
		Debug.Log("Done!");
		playerReputation.text = SetReputationData(systemData.systemOwner);
	}

	string SetReputationData(string owner) {
		Debug.Log(owner);
		switch (owner) {
			case "Dominion of Earth":
				return "Faction: Dominion of Earth \n" +
				 				"Reputation: " + _playerPilotData.dominionReputation;
			case "Divine Trade Alliance":
				return "Faction: Divine Trade Alliance \n" +
								"Reputation: " + _playerPilotData.allianceReputation;
			default:
				return "";
		}
	}



}
