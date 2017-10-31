using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour {

  /*AI State

  Generic state for AI to use which all other states will inherit from.
  The state contains a list of "tasks," which are short, repeatable
  instructions. The state will attempt to loop through each task until it is
  either complete or another state is activated.

  */

  protected int currentTask = 0;
  protected bool readyToGo = false;

  [HideInInspector]
  public bool thrustersOn;
  [HideInInspector]
  public bool brakesOn;
  [HideInInspector]
  public float rotation;

  public AIStateTrigger trigger;
  public stateTask[] stateTasks;

  float angleTowardsTarget;


  bool checkTaskCompleted(stateTask task) {
    if (task.taskCompleted) {
      return true;
    }
      return false;
    }


  void moveToLocation(stateTask task) {

    Vector2 target = task.location - new Vector2(transform.position.x, transform.position.y);
    float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg - 90;

    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //Rotate towards location;
    //Increase speed until finished
  }

  float SetRotationTarget(stateTask task) {
    if (!task.target) {
      task.target = Instantiate(new GameObject(), task.location, Quaternion.identity);
      task.target.name = name + " -- Waypoint";
    }
  angleTowardsTarget = Vector2.Angle(transform.position, task.location);
  return angleTowardsTarget;
  }


  void Awake() {
    SetRotationTarget(stateTasks[currentTask]);
  }


  void Update() {
    moveToLocation(stateTasks[currentTask]);
  }

}


[System.Serializable]
public class stateTask {
  public string taskName = "Task"; //Just to save headaches later
  public taskType task;
  public bool taskCompleted;

  public Vector2 location;
  public GameObject target;
}

public enum taskType {
  doNothing,
  disableTarget,
  killTarget,
  fleeFromTarget,
  goToLocation,
  landAtLocation,
  hyperspace
}
