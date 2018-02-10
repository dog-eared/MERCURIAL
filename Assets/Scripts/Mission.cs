using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Mission : ScriptableObject {

	public string description = "Long description of mission.";
	public List<MissionObjective> objectives = new List<MissionObjective>();
	public Rewards rewards = new Rewards();

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

}
