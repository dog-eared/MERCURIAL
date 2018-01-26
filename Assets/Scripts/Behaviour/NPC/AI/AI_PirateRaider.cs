using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PirateRaider : BaseAI {

  /* AI - Pirate Raider

  Pirate Raider is used for very stupid pirates (and aliens);
    - They don't run away.
    - They prioritize targets in the order: neutral, alliance, player, dominion, scourge
    - They change target to whoever attacked them as soon as they're hit.

  */

  bool hasTarget;
  public float findTargetFrequency = 1.5f;

  List<GameObject> targets = new List<GameObject>();

  protected override void Awake() {
    base.Awake();
    Invoke("GetTargets", findTargetFrequency);
    Debug.Log("Getting targets:");
  }

  public void GetTargets() {
    //For now, we'll go thru all game objects
    GameObject[] dominionTargets = GameObject.FindGameObjectsWithTag("Faction1Ship");
    GameObject[] allianceTargets = GameObject.FindGameObjectsWithTag("Faction2Ship");
    GameObject[] neutralTargets = GameObject.FindGameObjectsWithTag("Faction7Ship");
    //GameObject playerTarget = GameObject.FindGameObjectWithTag("PlayerShip");
    GameObject[] scourgeTargets = GameObject.FindGameObjectsWithTag("Faction4Ship");

    AddTargetsToList(neutralTargets);
    AddTargetsToList(allianceTargets);
    //targets.Add(playerTarget);
    AddTargetsToList(dominionTargets);
    AddTargetsToList(scourgeTargets);


    Debug.Log("Got targets");
    Debug.Log("First 3 targets:");
    Debug.Log(targets[0]);
    Debug.Log(targets[1]);
    Debug.Log(targets[2]);
  }


  public void AddTargetsToList(GameObject[] targetsList) {
    for (var x = 0; x < targetsList.Length; x++) {
      if (targetsList[x] != null && targetsList[x].layer == 13) {
        targets.Add(targetsList[x]);
      }
    }
  }


  void Update() {
    if (Input.GetKeyDown("6")) {
      AggroTarget(targets[0]);
    }

    if (Input.GetKeyDown("8")) {
      AggroTarget(targets[1]);
    }

  }


}
