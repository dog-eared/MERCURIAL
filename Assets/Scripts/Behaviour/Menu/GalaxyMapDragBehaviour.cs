using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMapDragBehaviour : MonoBehaviour {

	float offsetX;
	float offsetY;


	public void MouseDragStart() {
		offsetX = transform.position.x - Input.mousePosition.x;
		offsetY = transform.position.y - Input.mousePosition.y;
	}

	public void MouseDragCurrent() {
		transform.position = new Vector2(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);

	} 


}
