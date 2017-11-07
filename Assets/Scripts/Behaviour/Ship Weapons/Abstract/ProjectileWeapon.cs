using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : ShipWeapon {

	public float rateOfFire = 1f;
	public float bulletLifespan = 3f;
	float timeSinceLastShot = 0f; //must start at zero so we can shoot immediately

	AudioSource _audioSource;
	public AudioClip weaponClip;

	public GameObject projectile;

	int bulletPoolSize;
	public GameObject bulletPoolPrefab;
	GameObject bulletPool;

	string factionTag;

	int currentPoolIndex = 0;


	public virtual void Awake() {

		_audioSource = GetComponent<AudioSource>();
		_shipData = GetComponent<ShipData>();

		factionTag = _shipData.SetFactionString(_shipData.faction);
		GenerateBulletPool(factionTag + "Ship");

	}


	override public void GenerateBulletPool(string ownerTag) {

		/*
		Generating our bullet pool

		*/

		//Comment out the +1 if you want to minimize memory use... it's a safety in
		//case of some weird inbetween time which clips the lifespan of one of our
		//bullets

		bulletPoolSize = Mathf.RoundToInt(bulletLifespan / rateOfFire) + 1;

		bulletPoolPrefab = (GameObject)Resources.Load("Prefabs/Utility/Bullet Pool") as GameObject;
		bulletPool = Instantiate(bulletPoolPrefab);


		bulletPool.name = this.name + " - W: " + weaponName + " bullet pool";
		bulletPool.tag = ownerTag;

		//bulletPool.transform.parent = this.transform;

		for (var x = 0; x < bulletPoolSize; x++) {
			GameObject newBullet = Instantiate(projectile);
			newBullet.SetActive(false);
			newBullet.transform.parent = bulletPool.transform;
			newBullet.tag = ownerTag;
		}
	}


	override public void FireButtonPressed() {

		if (Time.time > rateOfFire + timeSinceLastShot) {

			//Audio for laser
			if (_audioSource.clip != weaponClip) {
				_audioSource.clip = weaponClip;
			}
			_audioSource.Play();
			//audio

			GameObject currentBullet = bulletPool.transform.GetChild(currentPoolIndex).gameObject;


			currentBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
			currentBullet.transform.rotation = transform.rotation;

			currentBullet.SetActive(true);

			Debug.Log(weaponName + " fired. Fired shot:" + currentPoolIndex);
			currentPoolIndex++;
			timeSinceLastShot = Time.time;

			if (currentPoolIndex >= bulletPoolSize) {
				currentPoolIndex = 0;
			}

		} else {
			Debug.Log("Not long enough since last shot.");
		}



	}


	override public void FireButtonReleased() {
		Debug.Log(weaponName + " released");
	}

}
