using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PirateRaider : BaseAI {

  List<GameObject> targets;

  protected override void Awake() {
    base.Awake();
    GetDominionTargets();
  }

  public void GetDominionTargets() {
    //For now, we'll go thru all game objects
    GameObject[] dominionTargets = GameObject.FindGameObjectsWithTag("Faction1Ship");
    GameObject playerTarget = GameObject.FindGameObjectWithTag("PlayerShip");

    targets.Add(playerTarget);
    for (var x = 0; x < dominionTargets.Length; x++) {
      targets.Add(dominionTargets[x]);
    }
  }

  public void TargetPlayer() {
    AggroTarget(targets[0]);
  }

  void Update() {
    if (Input.GetKey("6")) {
      TargetPlayer();
    }
  }

}
