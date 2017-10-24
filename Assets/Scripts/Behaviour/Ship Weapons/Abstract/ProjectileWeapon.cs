using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : ShipWeapon {

	public float rateOfFire = 1f;
	public float bulletLifespan = 3f;
	float timeSinceLastShot;


	public GameObject projectile;

	int bulletPoolSize;
	public GameObject bulletPoolPrefab;
	GameObject bulletPool;

	int currentPoolIndex = 0;



	public virtual void Awake() {
		/*
		Generating our bullet pool

		*/

		//Comment out the +1 if you want to minimize memory use... it's a safety in
		//case of some weird inbetween time which clips the lifespan of one of our
		//bullets
		bulletPoolSize = Mathf.RoundToInt(bulletLifespan / rateOfFire) + 1;

		bulletPoolPrefab = (GameObject)Resources.Load("Prefabs/Utility/Bullet Pool") as GameObject;
		bulletPool = Instantiate(bulletPoolPrefab);
		bulletPool.name = "W: " + weaponName + " Pool";

		bulletPool.transform.parent = this.transform;

		for (var x = 0; x < bulletPoolSize; x++) {
			GameObject newBullet = Instantiate(projectile);
			newBullet.SetActive(false);
			newBullet.transform.parent = bulletPool.transform;
		}

	}


	override public void FireButtonPressed() {

		if (Time.time > Time.time + timeSinceLastShot) {

			Debug.Log(weaponName + " fired");

			currentPoolIndex++;

			if (currentPoolIndex > bulletPoolSize) {
				currentPoolIndex = 0;
			}

		}

	}


	override public void FireButtonReleased() {
		Debug.Log(weaponName + " released");
	}

}
