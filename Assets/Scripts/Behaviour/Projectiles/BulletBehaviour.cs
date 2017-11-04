using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public int damage = 10;
	public bool passThrough = false;
	public float moveSpeed = 10f;
	public float timeTilDestroy = 3f;

	void FixedUpdate() {
		transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D collision) {
		Debug.Log("HIT!");
		collision.gameObject.GetComponent<ShipData>().TakeDamage(damage);
		if (!passThrough) {
			DestroySelf();
		}
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
