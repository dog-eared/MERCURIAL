using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public float moveSpeed = 10f;
	public float timeTilDestroy = 3f;

	void FixedUpdate() {
		transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
	}

	void OnEnable() {
		Invoke("DestroySelf", timeTilDestroy);
	}

	void DestroySelf() {
		gameObject.SetActive(false);
	}

	void OnDisable() {
		CancelInvoke();
	}

}
