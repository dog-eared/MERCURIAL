using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour {

	PilotData playerPilotData;
	
	List<Mission> missions = new List<Mission>();



	void Awake() {
		//default missions

		Mission dummyMission = new Mission();

		missions.Add(dummyMission);

	}


	void GiveReward(int rewardType, int rewardValue) {
		switch (rewardType) {
			case 0: //Credits reward
				playerPilotData.credits += rewardValue;
				break;
			case 1: //Dominion rep reward
				playerPilotData.dominionReputation += rewardValue;
				break;
			default:
				break;
		}
	}


}

class Mission {

	string description = "Long description of mission.";
	List<MissionObjective> objectives = new List<MissionObjective>();

}


class MissionObjective {
	ObjectiveType objectiveType = 0;
	string description = "One line bullet point.";
	bool completed = false;

	int targetQuantity = -1;
	GameObject shipTarget;
	string areaTarget;

	List<RewardType> rewardTypes = new List<RewardType>();
	List<int> rewardValues = new List<int>();

}

enum ObjectiveType {
	AccumulateStatistic,
	EnterSystem,
	LandonPlanet,
	KillXShip,
	KillSpecificShip
}

enum RewardType {
	Credits,
	DominionReputation,
	AllianceRepuation,
	RebelReputation,
	KarmaReputation,

}
