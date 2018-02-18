using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

	public GUIBehaviour _guiBehaviour;

	public Mission newMission; //Debug data
	public List<Mission> missions = new List<Mission>();
	public StatsMenuBehaviour _statsMenu;


	PilotData _playerPilotData;


	void Awake() {
		_playerPilotData = GameObject.FindGameObjectWithTag("PlayerObject").GetComponent<PilotData>();

		//Debug values.
		AddMission("Missions/Visit Mars");
		AddMission("Missions/Kill Heavy Fighter");
		AddMission("Missions/Go to Sirius");
	}

	void AddMission(string missionPath) {

		Mission loadedMission = Resources.Load(missionPath) as Mission;
		newMission = Instantiate(loadedMission);

		missions.Add(newMission);
		_guiBehaviour.ReceiveMessage("MISSION: " + newMission.description, true);

	}


	public List<Mission> GetMissions() {
		return missions;
	}

	public void CheckShipKilled(string shipName) {
		//NOTE: Needs huge refactor.

		//Loop through each objective of each mission
		for (int x = 0; x < missions.Count; x++) {
			for (int y = 0; y < missions[x].objectives.Count; y++) {

						//Check if shipname is right for this objectiveType
						//Also, check that we're killing a specific ship
						//If yes, objective complete
				if (missions[x].objectives[y].targetNameMatches(shipName)) {
					if (missions[x].objectives[y].objectiveType == ObjectiveType.KillSpecificShip) {
						_statsMenu.MissionListChanged();
						missions[x].objectives[y].completed = true;
						CheckMissionFinished(missions[x]);
					} else if (missions[x].objectives[y].objectiveType == ObjectiveType.KillXShip) {
						//Decrement target
						missions[x].objectives[y].targetQuantity -= 1;
						_statsMenu.MissionListChanged();
						CheckMissionFinished(missions[x]);
						if (missions[x].objectives[y].targetQuantity == 0) {
							_guiBehaviour.ReceiveMessage("COMPLETE: " + missions[x].objectives[y].description, true);
							_statsMenu.MissionListChanged();
							missions[x].objectives[y].completed = true;
							CheckMissionFinished(missions[x]);
						}
					}
				}
			}
		}
	}


	public void CheckNewArea(string areaName) {
		for (int x = 0; x < missions.Count; x++) {
			Debug.Log(missions[x].missionName);
			Debug.Log(missions[x].objectives.Count);
			for (int y = 0; y < missions[x].objectives.Count; y++) {
				Debug.Log(missions[x].objectives[y].objectiveType);
				if (missions[x].objectives[y].targetNameMatches(areaName)
						&& !missions[x].objectives[y].completed) {

							_guiBehaviour.ReceiveMessage("COMPLETE: " + missions[x].objectives[y].description, true);
					_statsMenu.MissionListChanged();
					missions[x].objectives[y].completed = true;
					CheckMissionFinished(missions[x]);
				}
			}
		}
	}


	void CheckMissionFinished(Mission mission) {
		//Starts with mission as
		bool missionComplete = true;

		for (int x = 0; x < mission.objectives.Count; x++) {
			if (mission.objectives[x].completed == false) {
				missionComplete = false;
			}
		}

		if (missionComplete) {
			_playerPilotData.GiveRewards(mission.GetRewards());
			newMission = mission;
			missions.Remove(mission);
		}
	}





}

[System.Serializable]
public class MissionObjective {


	public ObjectiveType objectiveType = 0;
	public string description = "New Objective.";

	public bool completed = false;

	public int currentQuantity = 0;
	public int targetQuantity = -1;

	public string shipTarget = "";
	public string areaTarget = "";


	public bool targetNameMatches(string checkString) {

		switch(objectiveType) {
			case ObjectiveType.EnterSystem:
			case ObjectiveType.LandOnPlanet:
				return checkString == areaTarget;
			case ObjectiveType.KillXShip:
			case ObjectiveType.KillSpecificShip:
				return checkString == shipTarget;
			default:
				return false;
		}

	}

}

[System.Serializable]
public class Rewards {
	public int credits;
	public int dominionReputation;
	public int allianceReputation;
	public int rebellionReputation;
	public int karma;
}

[System.Serializable]
public enum ObjectiveType {
	AccumulateStatistic,
	EnterSystem,
	LandOnPlanet,
	KillXShip,
	KillSpecificShip
}
