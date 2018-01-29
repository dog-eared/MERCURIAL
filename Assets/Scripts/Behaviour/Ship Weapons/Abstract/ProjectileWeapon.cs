using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : ShipWeapon {

	public float rateOfFire = 1f;
	public float bulletLifespan = 3f;
	protected float timeSinceLastShot = 0f; //must start at zero so we can shoot immediately

	public float bulletSpread = 0f; //for the inspector
	protected float bulletSpreadRandom = 0f; //for internal use

	protected  AudioSource _audioSource;
	public AudioClip weaponClip;

	public GameObject projectile;

	protected int bulletPoolSize;
	public GameObject bulletPoolPrefab;
	protected GameObject bulletPool;

	protected string factionTag;

	protected int currentPoolIndex = 0;


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
		//bulletPool.tag = ownerTag;


		for (var x = 0; x < bulletPoolSize; x++) {
			GameObject newBullet = Instantiate(projectile);
			newBullet.SetActive(false);
			newBullet.transform.parent = bulletPool.transform;
			newBullet.GetComponent<BulletBehaviour>().owner = this.gameObject;
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

			//Do we have any kind of spread? If yes, get a random value to add to the rotation;
			if (bulletSpread != 0) {
				bulletSpreadRandom = Random.Range(-bulletSpread, bulletSpread);
			}

			currentBullet.SetActive(false); //This will allow the values to reset in the bullet behaviour

			currentBullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
			currentBullet.transform.rotation = transform.rotation * Quaternion.Euler(0, 0, bulletSpreadRandom);

			currentBullet.SetActive(true);

			currentPoolIndex++;
			timeSinceLastShot = Time.time;

			if (currentPoolIndex >= bulletPoolSize) {
				currentPoolIndex = 0;
			}

		}
	}

	public void ClearBulletPool() {
		Destroy(bulletPool);
	}


	void OnDestroy() {
		ClearBulletPool();
	}


	override public void FireButtonReleased() {
		//Debug.Log(weaponName + " released");
	}

}
