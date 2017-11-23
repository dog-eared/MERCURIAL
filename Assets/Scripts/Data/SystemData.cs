using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SystemData {
	//Simple container that stores our array of planets, meaning we can access
	//them using dot notation.

	//Is also passed back to SystemManager so System information can be worked in

	public string systemName;
	public string backdropMaterial; //Important this string, not Material. It will be cast as a material at assignment to PlanetData;
	public string systemOwner;
	public string visitorList;
	public bool systemPopulated;
	public float systemSize;
	public int asteroidsToSpawn;
	public float chanceOfPirate;

	public List<string> planets;
}
