using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour {

	public float asteroidHealth = 50f;
	public GameObject explosionAnimation;

	public void TakeDamage(float damage) {
		asteroidHealth -= damage;
		if (asteroidHealth < 0) {
			Death();
		}
	}

	void Death() {
		Instantiate(explosionAnimation, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
		Destroy(gameObject);
}

}
