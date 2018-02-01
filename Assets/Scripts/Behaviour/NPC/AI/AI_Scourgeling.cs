using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Scourgeling : BaseAI {

  /* AI - Scourgling

  Identical to pirate raider, but it attacks pirates rather than scourge;
    - They don't run away.
    - They prioritize targets in the order: neutral, alliance, player, dominion, pirate
    - They change target to whoever attacked them as soon as they're hit.

  */

  bool hasTarget;
  public float findTargetFrequency = 1.5f;
  public float purgeListFrequency = 5f;

  float delaySearch = 3f;


  protected override void Awake() {
    base.Awake();
    Invoke("GetTargets", findTargetFrequency);
    InvokeRepeating("CheckTargetDead", delaySearch, purgeListFrequency);
  }

  protected override void GetTargets() {
    //For now, we'll go thru all game objects
    GameObject[] dominionTargets = GameObject.FindGameObjectsWithTag("Faction1Ship");
    GameObject[] allianceTargets = GameObject.FindGameObjectsWithTag("Faction2Ship");
    GameObject[] neutralTargets = GameObject.FindGameObjectsWithTag("Faction7Ship");
    GameObject playerTarget = GameObject.FindGameObjectWithTag("PlayerShip");
    GameObject[] pirateTargets = GameObject.FindGameObjectsWithTag("Faction5Ship");

    AddTargetsToList(neutralTargets);
    AddTargetsToList(allianceTargets);
    targets.Add(playerTarget);
    AddTargetsToList(dominionTargets);
    AddTargetsToList(pirateTargets);


  }

}
