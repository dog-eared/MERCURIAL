using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSelfDestroy : MonoBehaviour {

	/*
	DECAL SELF DESTROY

	For decals like explosions and other effects that should clear themselves
	after a set period of time.


	*/

	public float lifetimeInSeconds;
	float spawnTime;

	void Awake() {
		spawnTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > spawnTime + lifetimeInSeconds) {
			Destroy(gameObject);
		}
	}
}
