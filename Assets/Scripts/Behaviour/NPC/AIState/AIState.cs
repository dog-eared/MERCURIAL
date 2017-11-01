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

  public bool firingWeapon1;
  public bool firingWeapon2;

  public AIStateTrigger trigger;
  public stateTask[] stateTasks;

  public bool facingTarget = false;

  public float rotationTarget = 0f; //Represents the Rotation.Z value we want to aim for
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

    //Done as -90 because it likes to point sideways otherwise.
    rotationTarget = Mathf.Repeat((Mathf.Round(Mathf.Atan2(location.y, location.x) * Mathf.Rad2Deg) - 90), 359);
  }


  void RotateToTarget() {

    float currentRotationNormalized = Mathf.Floor(transform.localEulerAngles.z);

    /* BUG:
    This section has been written out with long, explicit comparisons because it
    there's currently an issue -- if the target crosses a point directly UP on
    the screen from the current location, the player will rotate all the way
    around in the wrong direction. Might not be a huge problem, but being expli-
    cit about what's going on should help fixing this later.
    */

    if (currentRotationNormalized > (rotationTarget - rotationTolerance) && currentRotationNormalized < (rotationTarget + rotationTolerance)) {
      rotation = 0f;
      facingTarget = true;
    } else if (currentRotationNormalized < (rotationTarget - rotationTolerance)) {
      rotation = 1f;
    } else if (currentRotationNormalized > (rotationTarget + rotationTolerance)) {
      rotation = -1f;
    }

  }


  void Update() {
    SetRotationTarget();
    //Debug.Log("RT- :" + (rotationTarget - rotationTolerance) + "  < " + (transform.localEulerAngles.z) + " < RT+ :" + (rotationTarget + rotationTolerance) );
    //Debug.Log(transform.localEulerAngles.z);
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
