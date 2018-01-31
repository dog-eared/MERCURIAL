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
  public float swapTargetFrequency = 8f;

  public List<GameObject> targets = new List<GameObject>();

  protected override void Awake() {
    base.Awake();
    Invoke("GetTargets", findTargetFrequency);
    InvokeRepeating("GetNearestTarget", swapTargetFrequency, swapTargetFrequency);
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


  public void GetNearestTarget() {

    float distance = CalculateDistanceToPoint(targets[0], this.gameObject);
    int swapPlace = 0;
    GameObject temp = targets[0];

    for (int x = 0; x < targets.Count; x++) {
      if (CalculateDistanceToPoint(targets[x], this.gameObject) < distance) {
        swapPlace = x;
      }
    }

    targets[0] = temp;
    targets[0] = targets[swapPlace];
    targets[swapPlace] = temp;

    if (combatAI.enabled) {
      AggroTarget(targets[0]);
    }
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


  float CalculateDistanceToPoint(GameObject a, GameObject b) {
    return Mathf.Abs(a.transform.position.y - b.transform.position.y) /
            (a.transform.position.x - b.transform.position.x);
  }



}
