using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debug_WaypointDraw : MonoBehaviour {

	public float visualGizmoDistance;

	void OnDrawGizmos() {
		Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.DrawSphere(transform.position, visualGizmoDistance);
	}
}
