using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationVisitMenu : MonoBehaviour {

	GameObject manager;
	MenuManager menuManager;

	// Use this for initialization
	void Awake () {
		manager = GameObject.FindGameObjectWithTag("GameController");
		menuManager = manager.GetComponent<MenuManager>();
	}

	void Update() {
		if (Input.GetKeyDown("l")) {
			LeavePlanet();
		}
	}

	public void LeavePlanet() {
		menuManager.CloseTopMenu();
	}

}
