using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {

	public GameObject asteroid;

	public void SpawnAsteroids(int numberToSpawn, float areaSize) {
		for (var x = 0; x < numberToSpawn; x++) {
			float spawnX = Random.Range(-areaSize, areaSize); //Repeat code so we get two different
			float spawnY = Random.Range(-areaSize, areaSize); //random spots, not matching like x1y1, x3y3 etc
			Vector3 spawnPoint = new Vector3(spawnX, spawnY, 1);

			GameObject newAsteroid = Instantiate(asteroid, spawnPoint, Quaternion.Euler(0, 0, Random.Range(0, 359)));
			Rigidbody2D newAsteroidRigidbody2D = newAsteroid.GetComponent<Rigidbody2D>();

			newAsteroidRigidbody2D.velocity = Random.onUnitSphere * Random.Range(1, 15);
		}
	}

}
