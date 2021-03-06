﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlanetData : MonoBehaviour {

	/*
	PLANET DATA

	--Storage class for our data so it can be accessed by other functions during gameplay.
	--Not to be manually set -- should be generated by PlanetManager.cs
	--Contains an "update" function to be called after data has been loaded.

	*/

	MeshRenderer meshRenderer;

	public string planetName;
	public int minimapSize;
	public string mapGraphic;

	string blurb;

	public List<bool> buttons;

	void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
	}


	public void UpdatePlanetDisplay() {
		this.name = planetName;
		this.meshRenderer.material = Resources.Load("Visuals/Planets/" + mapGraphic, typeof(Material)) as Material;
		//Evidently there's no clean way to set material except by manually finding it in Resources

	}
}


[System.Serializable]
public class Planet {
	//Class used to plug in json data properly
	//Each piece of data we use MUST be defined in this class.
	public string planetName = "Unknown Planet";
	public int minimapSize;
	public float xLocation;
	public float yLocation;
	public float scale;
	public string mapGraphic;

	//Data for landing:
	public string blurb;
	public string landingGraphic;
	public List<bool> buttons;


}
