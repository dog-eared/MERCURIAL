using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

	float timeTilDestroy = 3f;

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
