using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mission : ScriptableObject {

	public string missionName = "Untitled Mission";
	public string fileName = "Mission Entry";


	public bool isPrimaryMission = false;

	public Sprite icon;

	public string description = "Overall description of mission.";
	public List<MissionObjective> objectives = new List<MissionObjective>();
	public Rewards rewards = new Rewards();

 	[TextArea(5,15)]
	public string longDescription = "Long description used in mission manager.";

	public bool stepsInOrder = false;
	public int currentStep = -1;


	public int[] GetRewards() {

		int[] rewardsArray = new int[5];

		rewardsArray[0] = rewards.credits;
		rewardsArray[1] = rewards.dominionReputation;
		rewardsArray[2] = rewards.allianceReputation;
		rewardsArray[3] = rewards.rebellionReputation;
		rewardsArray[4] = rewards.karma;

		return rewardsArray;

	}

	public string GetRewardsAsString() {

		int[] rewardsArray = GetRewards();

		//Init a string and an array with the labels for each reward
		string returnString = "\nREWARD:\n";
		string[] labels = new string[] {"Credits", "Dominion Rep", "Alliance Rep", "Rebellion Rep", "Karma"};

		//Loop thru rewards -- if it's any value but 0, add a label and value to the returned string
		for (int i = 0; i < rewardsArray.Length; i++) {
			if (rewardsArray[i] != 0) {
				returnString = returnString + labels[i] + ": " + rewardsArray[i] + "\n";
			}
		}

		return returnString;
	}

}
