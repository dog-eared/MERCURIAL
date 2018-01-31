using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationPanel : MonoBehaviour {



	/* COMMUNICATION PANEL

	Contains all functions for the communication panel to give good, useful data.

	*/


	//References for assigning in inspector:
	public GameObject shipDisplayText;
	public GameObject shipDisplaySprite;

	Text _displayText;
	Image _displaySprite;

	GameObject currentTarget;
	ShipData _targetData;


	string targetName;
	Sprite targetSprite;


	void Awake() {
		_displayText = shipDisplayText.GetComponent<Text>();
		_displaySprite = shipDisplaySprite.GetComponent<Image>();
		Invoke("UpdateDisplay", 3);
	}


	void GetTargetData() {
		_targetData = currentTarget.GetComponent<ShipData>();
		targetName = _targetData.shipName;
	}


	void UpdateDisplay() {
		_displayText.text = "NewText";

	}


}
