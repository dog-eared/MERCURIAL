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

  protected override void Awake() {
    base.Awake();
    Invoke("GetTargets", findTargetFrequency);
    InvokeRepeating("GetNearestTarget", swapTargetFrequency, swapTargetFrequency);

  }

  protected override void GetTargets() {
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

}
