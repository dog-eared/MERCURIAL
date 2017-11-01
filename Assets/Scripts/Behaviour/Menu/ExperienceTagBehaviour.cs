using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceTagBehaviour : MonoBehaviour {

	public int combatBonus = 0;
	public int diplomacyBonus = 0;
	public int intimidationBonus = 0;
	public int mechanicsBonus = 0;
	public int thriftBonus = 0;

	public int ReturnIndividualBonus(string bonusName) {
		switch (bonusName) {
			case "Combat":
				return combatBonus;
			case "Diplomacy":
				return diplomacyBonus;
			case "Intimidation":
				return intimidationBonus;
			case "Mechanics":
				return mechanicsBonus;
			case "Thrift":
				return thriftBonus;
		}
		return 0;
	}

	public int[] ReturnBonusAsArray() {
		int[] returnArray = new int[5];
		returnArray[0] = combatBonus;
		returnArray[1] = diplomacyBonus;
		returnArray[2] = intimidationBonus;
		returnArray[3] = mechanicsBonus;
		returnArray[4] = thriftBonus;
		return returnArray;
	}



}
