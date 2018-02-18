using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;

public class MissionEntryBehaviour : MonoBehaviour {

	public Image icon;
	public Text missionName;
	public Text missionDescription;

	public Mission missionData;

	public string rewardsText;


	void Awake() {
	}


	void OnEnable() {
		SetMissionData(missionData);
	}


	public void SetMissionData(Mission mission) {

		this.missionName.text = mission.missionName;

		this.icon.sprite = mission.icon;

		this.missionDescription.text = mission.description + "\n"
																 + "NEXT: " + GetNextObjective(mission.objectives);

    this.rewardsText = mission.GetRewardsAsString();

		if (mission.isPrimaryMission) {
			this.gameObject.tag = "Mission_Primary";
		}

		//missionData = mission; //Debug

	}


	string GetNextObjective(List<MissionObjective> objectives) {

		for (int x = 0; x < objectives.Count; x++) {
			if (!objectives[x].completed) {
				return objectives[x].description;
			}
		}
		return "";
	}


	public void OnClick() {
		SendMessageUpwards("SelectMission", missionData);
	}




}
