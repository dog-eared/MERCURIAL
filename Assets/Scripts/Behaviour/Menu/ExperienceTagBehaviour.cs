using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceTagBehaviour : MonoBehaviour {

	//Image tagIconObject;
	Text tagTitleObject;
	Text tagBonusObject;

	string tagTitle = "Robotic Mind";
	string tagBonus = "+3 Mechanics\n+1 Diplomacy";

	void Awake() {
		//tagIconObject = this.gameObject.transform.GetChild(0).GetComponent<Image>();
		tagTitleObject = this.gameObject.transform.GetChild(1).GetComponent<Text>();
		tagBonusObject = this.gameObject.transform.GetChild(2).GetComponent<Text>();
	}

	void OnEnable() {
		tagTitleObject.text = tagTitle;
		tagBonusObject.text = tagBonus;
	}


}
