using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {

	public Image _hullBar;
	public Image _shieldBar;

	// Use this for initialization
	void Awake () {

	}

	public void SetBarAmount(string bar, float fillPercentage) {
		if (bar == "hull") {
			_hullBar.fillAmount = fillPercentage;
		} else if (bar == "shield") {
			_shieldBar.fillAmount = fillPercentage;
		}
	}

}
