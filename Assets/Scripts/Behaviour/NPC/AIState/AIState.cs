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

  public bool facingTarget = false;

  float rotationTarget = 0f; //Represents the Rotation.Z value we want to aim for
  public float rotationTolerance = 10f; //Represents how far on either side of rotationTarget we can be

  bool checkTaskCompleted(stateTask task) {
    if (task.taskCompleted) {
      return true;
    }
      return false;
    }

  void SetRotationTarget() {
    Vector3 location = stateTasks[currentTask].location - transform.position;
    location.Normalize();

    //Done as +90 because we're normalizing to +180... but also -90 because otherwise it'll be sideways
    rotationTarget = Mathf.Round(Mathf.Atan2(location.y, location.x) * Mathf.Rad2Deg) + 90;
  }

  void RotateToTarget() {
    if (transform.eulerAngles.z < (rotationTarget - rotationTolerance)) {
      rotation = 1f;
      facingTarget = false;
    } else if (transform.eulerAngles.z > (rotationTarget + rotationTolerance)) {
      rotation = -1f;
      facingTarget = false;
    } else {
      rotation = 0f;
      facingTarget = true;
    }
  }

  void Update() {
    SetRotationTarget();
    Debug.Log((rotationTarget - rotationTolerance) + "<" + (transform.eulerAngles.z) + "<" + (rotationTarget + rotationTolerance));
    Debug.Log(rotationTarget);
    RotateToTarget();
    //rotation = 1f;
  }




}


[System.Serializable]
public class stateTask {
  public string taskName = "Task"; //Just to save headaches later
  public taskType task;
  public bool taskCompleted;

  public Vector3 location;
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
