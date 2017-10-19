using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotData : MonoBehaviour {

	[Header("Name")]
	public string firstName = "Tom";
	public string lastName = "Cruise";
	public string callSign = "Maverick";

	[Space(10)]
	[Header("Pronouns:")]
	public string possessive = "his";
	public string singular = "he";

	[Space(10)]
	[Header("Skills:")]
	public float intimidationSkill = 0;
	public float diplomacySkill = 0;
	public float thriftSkill = 0;
	public float combatSkill = 0;
	public float mechanicsSkill = 0;
}
