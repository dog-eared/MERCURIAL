using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionEntryBehaviour : MonoBehaviour {

	public Image icon;
	public Text missionName;
	public Text missionDescription;

	void Awake() {
		SetMissionData(Resources.Load("Missions/Visit Mars") as Mission);
	}

	public void SetMissionData(Mission mission) {

		this.missionName.text = mission.missionName;

		this.icon.sprite = mission.icon;

		this.missionDescription.text = "NEXT: " + mission.description + "\n"
																 + GetNextObjective(mission.objectives);


	}


	string GetNextObjective(List<MissionObjective> objectives) {

		for (int x = 0; x < objectives.Count; x++) {
			if (!objectives[x].completed) {
				return objectives[x].description;
			}
		}
		return "";
	}




}
