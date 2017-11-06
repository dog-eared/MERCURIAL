using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyBlaster : ProjectileWeapon {

	public override void Awake() {

		weaponName = "Heavy Blaster";

		if (projectile == null) {
			projectile = Resources.Load("/Projectiles/HeavyBlasterShot", typeof(GameObject)) as GameObject;
		}

		base.Awake();

	}

}
