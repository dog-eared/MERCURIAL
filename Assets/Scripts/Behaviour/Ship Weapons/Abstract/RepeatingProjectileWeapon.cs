using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingProjectileWeapon : ProjectileWeapon {

  bool isFiring;

  override public void FireButtonPressed() {
    isFiring = true;
    InvokeRepeating("Fire", 0, rateOfFire);
  }

  override public void FireButtonReleased() {
    isFiring = false;
    CancelInvoke();
  }

  void Fire() {
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




}
