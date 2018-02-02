using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotData : MonoBehaviour {

	/* PILOT DATA

	Handles pilot data as it's presented to the player. Should include:
		Name & pronouns
		Faction reputations
		General reputations (karma for good/bad, combat for kills)

	*/

	[Header("Name")]
	public string firstName = "Firstname";
	public string lastName = "Lastname";
	public string callsign = "Callsign";

	[Space(10)]
	[Header("Pronouns:")]
	public string possessive = "their";
	public string singular = "they";

	[Space(10)]
	[Header("Reputations:")]
	public int dominionReputation = 0;
	public int allianceReputation = 0;
	public int rebelReputation = 0;
	public int combatRating = 0;
	public int karma = 0;

	[Space(10)]
	[Header("Other Data:")]
	public int credits = 1000;

}
