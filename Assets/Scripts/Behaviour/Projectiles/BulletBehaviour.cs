using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

	public int damage = 10;
	public bool passThrough = false;
	public float moveSpeed = 10f;
	public float timeTilDestroy = 3f;
	public float fadeoutTime = 0.4f;

	public SpriteRenderer sprite;

	Vector2 lastPosition;
	Vector2 newPosition;

	float alphaValue = 1f;
	IEnumerator fade;

	void FixedUpdate() {


		/*
		What's going on here:
		We  set lastPosition, then move, then set newPosition.
		Then we check if there's a collision between those two points using Raycast
		but only checking on layer 8192 (which is the Ships layer)

		If yes, check the tag.

		*/
		lastPosition = transform.position;
		transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
		newPosition = transform.position;

		//8192 is the value used by our layerMask for Layer 13:ships.
		RaycastHit2D hit = Physics2D.Raycast(lastPosition, newPosition, 1, 8192);

		if (hit) {
			if (hit.collider.tag != transform.tag) {
				RaycastHitTarget(hit.collider);
			}
		}


	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(lastPosition, newPosition);
	}

	void RaycastHitTarget(Collider2D collision) {
		if (collision.tag == "Asteroid") {
			collision.gameObject.GetComponent<AsteroidBehaviour>().TakeDamage(damage);
		}	else {
			collision.gameObject.GetComponent<ShipData>().TakeDamage(damage);
		}
		if (!passThrough) {
			DestroySelf();
		}
	}


	void StartFade() {
		fade = FadeOut();
		StartCoroutine(fade);
	}


	IEnumerator FadeOut() {
		float t = 0;

		while (t < fadeoutTime) {
			alphaValue = Mathf.Lerp(1.0f, 0.0f, t / fadeoutTime);
			sprite.color = new Color(1, 1, 1, alphaValue);

			t += Time.deltaTime;
			yield return null;

		}

		DestroySelf();

	}


	void DestroySelf() {
		if (fade != null) {
			StopCoroutine(fade);
		}
		gameObject.SetActive(false);
	}


	void OnEnable() {
		//Setting newPosition/lastPosition here so they aren't null on instantiation
		sprite.color = new Color(1, 1, 1, 1);
		newPosition = lastPosition = transform.position;
		Invoke("StartFade", timeTilDestroy - fadeoutTime);
	}


	void OnDisable() {
		CancelInvoke();
	}




}
