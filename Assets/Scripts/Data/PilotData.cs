using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotData : MonoBehaviour {

	[Header("Name")]
	public string firstName = "Firstname";
	public string lastName = "Lastname";
	public string callsign = "Callsign";

	[Space(10)]
	[Header("Pronouns:")]
	public string possessive = "their";
	public string singular = "they";

	[Space(10)]
	[Header("Skills & Exp:")]
	public float combatSkill = 0;
	public float diplomacySkill = 0;
	public float intimidationSkill = 0;
	public float mechanicsSkill = 0;
	public float thriftSkill = 0;
	public List<string> pilotExperiences;

	[Space(10)]
	[Header("Reputations:")]
	public int dominionReputation = 50;
	public int allianceReputation = 50;
	public int rebelReputation = 50;

}
