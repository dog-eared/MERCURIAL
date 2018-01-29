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

  public List<GameObject> targets = new List<GameObject>();

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
    GameObject playerTarget = GameObject.FindGameObjectWithTag("PlayerShip");
    GameObject[] scourgeTargets = GameObject.FindGameObjectsWithTag("Faction4Ship");

    AddTargetsToList(neutralTargets);
    AddTargetsToList(allianceTargets);
    targets.Add(playerTarget);
    AddTargetsToList(dominionTargets);
    AddTargetsToList(scourgeTargets);


  }

  public void CheckTargetDead() {
    if (targets[0] == null) {
      targets.Remove(targets[0]);
    } else {
      AggroTarget(targets[0]);
    }
  }


  public void AddTargetsToList(GameObject[] targetsList) {
    for (var x = 0; x < targetsList.Length; x++) {
      if (targetsList[x] != null && targetsList[x].layer == 13) {
        targets.Add(targetsList[x]);
      }
    }
  }



}
