using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_DominionDefender : BaseAI {

  /* AI - Dominion Defender

  Pirate Raider is used for dominion ships.
    - They don't run away.
    - They prioritize targets in the order: scourge, pirate.
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
    GameObject[] scourgeTargets = GameObject.FindGameObjectsWithTag("Faction4Ship");
    GameObject[] pirateTargets = GameObject.FindGameObjectsWithTag("Faction5Ship");

    AddTargetsToList(scourgeTargets);
    AddTargetsToList(pirateTargets);


  }



}
